import { ShowLogoutButton } from "./shared.js";


export const LoadHomePage = () => {
    try {
        if (localStorage.getItem("token")) {
            ShowLogoutButton();
        }

        LoadHomePageContent();


    } catch (e) {
        console.log(e, "Unable to load homepage.");
    }
}


const LoadHomePageContent = async () => {
    try {
        const imagesDiv = document.getElementById("main-content");

        const filmsUrl = "api/films";

        const response = await fetch(filmsUrl);
        const films = await response.json();

        if (films.length > 0) {
            imagesDiv.innerHTML = films.map(film =>
                `
        <div class="col-sm-4 col-lg-4 col-md-4" id="film-images">
            <div class="thumbnail">
                    <img src=${film.imageUrl} alt=${film.filmName} class="img-fluid" loading="lazy">
                    <div class="caption justify-content-center">
                        <h4>${film.filmName}</h4>
                        <h6>Director : ${film.director}</h6>
                        <h6>Release year : ${film.releaseYear}</h6> 
                        <p>Country: ${film.country}</p>
                    </div>
                        
            </div>
        </div>`
            ).join("");
        }
        else {
            imagesDiv.innerHTML = `
        <h3>There is no film in the database! Somebody may have removed them!</h3>`;
        }

    } catch (e) {
        console.log(e);
    }

}

