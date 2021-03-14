import { ShowLogoutButton } from "./shared.js";
import {LoadRegisterPageContent} from "./register_page_module.js"

export const LoadProfilePage = async (e) => {
    try {
        e.preventDefault();

        if (localStorage.getItem("token")) {
            ShowContentOfProfilePage();
        } else {
            await LoadLoginPanel();
            document.getElementById("login-form").addEventListener("submit",
                (e) => {
                    if (e.preventDefault) e.preventDefault();
                    CreateToken();
                    return false;
                });

            document.getElementById("register-link-in-login-page").addEventListener("click", async (e) => {
                e.preventDefault();
                await LoadRegisterPageContent();
            });

        }

    } catch (e) {
        console.log(e, "Unable to load partial html.");
    }
}


const LoadProfile = async () => {
    document.querySelector("#main-content").innerHTML = "";
    const response = await fetch("./profile.html");
    const data = await response.text();
    document.querySelector("#main-content").innerHTML = data;

    document.getElementById("register-link").innerHTML = "";
    document.getElementById("logout").innerHTML = "Logout";
}

const LoadLoginPanel = async () => {
    document.querySelector("#main-content").innerHTML = "";
    const response = await fetch("./loginPage.html");
    const data = await response.text();
    document.querySelector("#main-content").innerHTML = data;
}



const CreateToken = async () => {

    try {
        const errorMessage = document.getElementById("login-error-message");
        errorMessage.innerHTML = "";

        const username = document.getElementById("username");
        const password = document.getElementById("password");


        let usernameValue = username.value.toLowerCase();
        let passwordValue = password.value;


        const credentials = {
            username: usernameValue,
            Password: passwordValue
        };


        const response = await fetch("api/authentication/login",
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(credentials)
            });

        const data = await response.json();
        localStorage.setItem("token", data.token);
        localStorage.setItem("user", data.user);


        if (localStorage.getItem("token") !== "undefined") {
            console.log("token was set");

            ShowContentOfProfilePage();


        }
        else {
            errorMessage.innerHTML = "Either username or password is wrong. Try again!";
        }
    } catch (e) {
        console.log(e);
    }

}

const ShowContentOfProfilePage = async () => {
    await LoadProfile();
    document.getElementById("list-booked-movies-btn").addEventListener("click", LoadRentedFilms);
    document.getElementById("book-movie-btn").addEventListener("click", LoadFilmsForAuthorizedUser);
    ShowLogoutButton();
}

const LoadRentedFilms = async () => {
    try {
        const token = localStorage.getItem("token");
        const rentalListUrl = `api/rentalrecords/search?includeOrderItem=true`;


        const bearerToken = "Bearer " + token;

        const response = await fetch(rentalListUrl,
            {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': bearerToken
                }

            });
        const rentedFilms = await response.json();

        if (rentedFilms.length > 0) {

            document.getElementById("profile-content-col").innerHTML = "";
            document.getElementById("profile-content-col").appendChild(CreateListOfRentalRecords(rentedFilms));

        } else {
            document.getElementById("profile-content-col").innerHTML = `
            <h3 class="text-center m-1 p-1">There is no rental record to show.</h3>`;
        }

    } catch (e) {
        console.log(e);
    }

}


const LoadFilmsForAuthorizedUser = async () => {
    try {
        const token = localStorage.getItem("token");

        const filmListUrl = `api/films`;

        const bearerToken = "Bearer " + token;

        const response = await fetch(filmListUrl,
            {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': bearerToken
                }

            });
        const films = await response.json();

        if (films.length > 0) {
            document.getElementById("profile-content-col").innerHTML = "";
            document.getElementById("profile-content-col").appendChild(CreateListOfFilmsForAuthorizedUser(films));

        } else {
            document.getElementById("profile-content-col").innerHTML = `
            <h3 class="text-center m-1 p-1">There is no movie.</h3>`;
        }
    } catch (e) {
        console.log(e);
    }

}



const CreateListOfRentalRecords = (rentedFilms) => {
    const div = document.createElement("div");
    div.className = "card-body";

    const title = document.createElement("h3");
    title.innerHTML = "List of rented movies";
    title.className = "text-center m-1 p-1";
    div.appendChild(title);

    const table = document.createElement("table");
    table.className = "table table-bordered table-striped";

    const tableHead = document.createElement("thead");
    const headerRow = document.createElement("tr");

    const th1 = document.createElement("th");
    th1.innerHTML = "Film name";

    const th2 = document.createElement("th");
    th2.innerHTML = "Rental date";

    const th3 = document.createElement("th");

    headerRow.appendChild(th1);
    headerRow.appendChild(th2);
    headerRow.appendChild(th3);
    tableHead.appendChild(headerRow);
    table.appendChild(tableHead);

    const tableBody = document.createElement("tbody");


    const bodyRows = rentedFilms.map(record => {
        const bodyRow = document.createElement("tr");

        const td1 = document.createElement("td");
        td1.innerHTML = record.film.filmName;

        const td2 = document.createElement("td");
        td2.innerHTML = record.rentalDate;

        const td3 = document.createElement("td");
        
        const button = document.createElement("button");
        button.className = "btn btn-primary";
        button.innerHTML = "Return";
        button.addEventListener("click", () => ReturnFilm(record.orderId));
        td3.appendChild(button);

        bodyRow.appendChild(td1);
        bodyRow.appendChild(td2);
        bodyRow.appendChild(td3);
        tableBody.appendChild(bodyRow);
        return bodyRow;
    });

    
    table.appendChild(tableBody);
    div.appendChild(table);

    return div;
}

const CreateListOfFilmsForAuthorizedUser = (allFilms) => {
    const div = document.createElement("div");
    div.className = "card-body";

    const title = document.createElement("h3");
    title.innerHTML = "List of all movies";
    title.className = "text-center m-1 p-1";
    div.appendChild(title);

    const table = document.createElement("table");
    table.className = "table table-bordered table-striped";

    const tableHead = document.createElement("thead");
    const headerRow = document.createElement("tr");

    const th1 = document.createElement("th");
    th1.innerHTML = "Film name";

    const th2 = document.createElement("th");
    th2.innerHTML = "Director";

    const th3 = document.createElement("th");
    th3.innerHTML = "Year";

    const th4 = document.createElement("th");
    th4.innerHTML = "No of available copies";

    const th5 = document.createElement("th");

    headerRow.appendChild(th1);
    headerRow.appendChild(th2);
    headerRow.appendChild(th3);
    headerRow.appendChild(th4);
    headerRow.appendChild(th4);
    tableHead.appendChild(headerRow);
    table.appendChild(tableHead);

    const tableBody = document.createElement("tbody");

    const bodyRows = allFilms.map(film => {
        const bodyRow = document.createElement("tr");

        const td1 = document.createElement("td");
        td1.innerHTML = film.filmName;

        const td2 = document.createElement("td");
        td2.innerHTML = film.director;

        const td3 = document.createElement("td");
        td3.innerHTML = film.releaseYear;

        const td4 = document.createElement("td");
        td4.innerHTML = film.totalNumberOfCopies === film.numberOfRentedCopies
            ? 'Not available'
            : film.totalNumberOfCopies - film.numberOfRentedCopies;

        const td5 = document.createElement("td");

        if (film.totalNumberOfCopies > film.numberOfRentedCopies) {
            const form = document.createElement("form");
            form.addEventListener("submit",
                (e) => {
                    e.preventDefault();
                    BookFilm(film.filmId);
                });
            const button = document.createElement("button");
            button.className = "btn btn-primary";
            button.innerHTML = "Book";
            button.type = "submit";

            form.appendChild(button);
            td5.appendChild(form);
        }

        bodyRow.appendChild(td1);
        bodyRow.appendChild(td2);
        bodyRow.appendChild(td3);
        bodyRow.appendChild(td4);
        bodyRow.appendChild(td5);
        tableBody.appendChild(bodyRow);
        return bodyRow;
    });


    table.appendChild(tableBody);
    div.appendChild(table);

    return div;
}



export const BookFilm = async (filmId) => {
    try {
        const token = localStorage.getItem("token");
        const user = localStorage.getItem("user");
        const bookFilmUrl = "api/rentalrecords";

        const bearerToken = "Bearer " + token;

        const values = {
            film: {
                filmId: filmId
            },
            filmstudio: {
                username: user
            }

        };

        const response = await fetch(bookFilmUrl,
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': bearerToken
                },
                body: JSON.stringify(values)
            });

        if (response.status === 201) {
            ShowMessage("Successfully booked!");
        } else {
            ShowMessage("Something went wrong");
        }
    } catch (e) {
        console.log(e);
    }

}


const ReturnFilm = async (orderId) => {
    try {
        const token = localStorage.getItem("token");
        const user = localStorage.getItem("user");
        const rentalListUrl = `api/rentalrecords/${orderId}`;

        const bearerToken = "Bearer " + token;

        const response = await fetch(rentalListUrl,
            {
                method: 'DELETE',
                headers: {
                    'Authorization': bearerToken
                }
            });
        if (response.status === 200) {
            ShowMessage("Movie successfully returned!");
        } else {
            ShowMessage("Something went wrong");
        }

    } catch (e) {
        console.log(e);
    }
}


const ShowMessage = (message) => {
    document.getElementById("profile-content-col").innerHTML = "";
    const div = document.createElement("div");
    const h3 = document.createElement("h3");
    h3.innerHTML = message;
    div.appendChild(h3);
    document.getElementById("profile-content-col").appendChild(div);
}