export const Logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    LoadHomePageContent();


}

export const ShowLogoutButton = () => {

    document.getElementById("logout").innerHTML = "Logout";
    document.getElementById("register-link").innerHTML = "";

}



