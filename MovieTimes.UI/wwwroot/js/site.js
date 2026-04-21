async function cargarCartelera() {
    try {
        const res = await fetch('http://localhost:5115/api/Movies/top-rated');
        if (!res.ok) throw new Error("Error en la API");
        const movies = await res.json();
        renderizarPeliculas(movies);
    } catch (error) {
        console.error("Fallo al cargar películas:", error);
    }
}

async function buscarPelicula() {
    const input = document.getElementById('txtBuscar');
    const id = input.value;
    if (!id) {
        alert("Por favor, ingresa un ID de película");
        return;
    }
    try {
        const res = await fetch(`http://localhost:5115/api/Movies/${id}`);
        if (!res.ok) {
            alert("No se encontró ninguna película con ese ID");
            return;
        }
        const movie = await res.json();
        renderizarPeliculas([movie]);
    } catch (error) {
        console.error("Error al buscar:", error);
        alert("Hubo un error en la conexión");
    }
}

function renderizarPeliculas(movies) {
    const contenedor = document.getElementById('contenedorPeliculas');
    if (!contenedor) return;
    contenedor.innerHTML = "";
    movies.forEach(movie => {
        const card = `
            <div class="col-md-3 mb-5">
                <div class="card card-vhs h-100">
                    <img src="https://image.tmdb.org/t/p/w500${movie.posterUrl}" class="card-img-top" alt="${movie.title}">
                    <div class="card-body">
                        <div class="vhs-label">${movie.title}</div>
                        <p class="text-muted mt-2" style="font-size: 0.7rem;">ID: ${movie.id}</p>
                        <p class="text-muted mt-2" style="font-size: 0.7rem;">${movie.overview}</p>
                        <button class="retro-btn w-100 mt-auto" onclick="verDetalle(${movie.id})">
                            RENTAR
                        </button>
                    </div>
                </div>
            </div>`;
        contenedor.innerHTML += card;
    });
}

async function verDetalle(id) {
    if (!userId || userId == '0') {
        Swal.fire({
            title: '⚠️ ACCESO DENEGADO',
            text: 'Debes iniciar sesión para rentar una película',
            icon: 'warning',
            confirmButtonText: 'INICIAR SESIÓN',
            confirmButtonColor: '#ff00ff',
            background: '#1a0a2e',
            color: '#00ffff'
        }).then(() => {
            window.location.href = '/User/Login';
        });
        return;
    }

    try {
        // 1. Obtener datos de la película
        const responseTMDB = await fetch(`http://localhost:5115/api/Movies/${id}`);
        if (!responseTMDB.ok) throw new Error("No se encontró la película en TMDB");
        const movieData = await responseTMDB.json();

        // 2. Guardar película en DB
        const movieToSave = {
            id: parseInt(movieData.id),
            title: movieData.title || "Sin título",
            overview: movieData.overview || "Sin descripción",
            release_Date: movieData.releaseDate || "2026-01-01",
            poster_Path: movieData.posterUrl || "",
            backdrop_Path: "",
            vote_Average: parseFloat(movieData.rating) || 0
        };

        const resMovie = await fetch('http://localhost:5024/api/Movie', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(movieToSave)
        });

        if (!resMovie.ok) {
            const errorText = await resMovie.text();
            console.error("Error guardando película:", errorText);
        }

        // 3. Guardar reserva
        const rental = {
            userId: parseInt(userId),
            movieId: parseInt(movieData.id),
            movieTitle: movieData.title,
            posterUrl: movieData.posterUrl,
            rentalDate: new Date().toISOString()
        };

        const resRental = await fetch('http://localhost:5024/api/Rental', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(rental)
        });

        if (resRental.ok) {
            mostrarConfirmacionReserva(movieData);
        } else {
            const errorText = await resRental.text();
            console.error("Error guardando reserva:", errorText);
            alert("Error al guardar la reserva.");
        }

    } catch (error) {
        console.error("Fallo en el fetch:", error);
    }
}

function mostrarConfirmacionReserva(movie) {
    const fechaHoy = new Date().toLocaleDateString('es-CR', {
        year: 'numeric', month: 'long', day: 'numeric'
    });

    const modal = document.createElement('div');
    modal.innerHTML = `
        <div id="modalReserva" style="
            position: fixed; top: 0; left: 0; width: 100%; height: 100%;
            background: rgba(0,0,0,0.85); z-index: 9999;
            display: flex; align-items: center; justify-content: center;">
            <div style="
                background: #1a0a2e; border: 2px solid #ff00ff;
                box-shadow: 0 0 30px #ff00ff; border-radius: 8px;
                padding: 2rem; max-width: 420px; width: 90%; text-align: center;
                font-family: 'Courier New', monospace; color: #00ffff;">
                <h2 style="color: #ff00ff; font-size: 1.4rem; margin-bottom: 0.5rem;">
                     RESERVA CONFIRMADA 
                </h2>
                <hr style="border-color: #ff00ff;">
                <img src="https://image.tmdb.org/t/p/w200${movie.posterUrl}"
                     style="border: 2px solid #00ffff; margin: 0.8rem 0;
                            border-radius: 4px; max-width: 120px;">
                <div style="text-align: left; margin: 1rem 0;
                            font-size: 0.85rem; line-height: 1.8;">
                    <p><span style="color: #ff00ff;"> RENTADO POR:</span> ${userName || 'Usuario'}</p>
                    <p><span style="color: #ff00ff;"> TÍTULO:</span> ${movie.title}</p>
                    <p><span style="color: #ff00ff;"> ESTRENO:</span> ${movie.releaseDate}</p>
                    <p><span style="color: #ff00ff;"> RATING:</span> ${movie.rating}</p>
                    <p><span style="color: #ff00ff;"> FECHA RENTA:</span> ${fechaHoy}</p>
                    <p style="font-size: 0.72rem; color: #aaa; margin-top: 0.5rem;">
                        ${movie.overview}
                    </p>
                </div>
                <hr style="border-color: #ff00ff;">
                <p style="color: #ffff00; font-size: 0.8rem;"> GUARDADO EN BASE DE DATOS</p>
                <button onclick="document.getElementById('modalReserva').remove()"
                    style="
                        margin-top: 1rem; background: #ff00ff; color: #000;
                        border: none; padding: 0.5rem 2rem;
                        font-family: 'Courier New'; font-weight: bold;
                        cursor: pointer; font-size: 1rem; border-radius: 4px;">
                    CERRAR
                </button>
            </div>
        </div>`;
    document.body.appendChild(modal);
}

document.addEventListener("DOMContentLoaded", () => {
    if (document.getElementById('contenedorPeliculas')) {
        cargarCartelera();
    }
});