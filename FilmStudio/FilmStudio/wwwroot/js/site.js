import { LoadHomePage } from "./modules/homepage_module.js";
import { LoadProfilePage } from "./modules/profile_page_module.js";
import { LoadRegisterPage } from "./modules/register_page_module.js"
import { Logout } from "./modules/shared.js";


LoadHomePage();

document.getElementById("logo-link").addEventListener("click", LoadHomePage);

document.getElementById("home-link").addEventListener("click", LoadHomePage);

document.getElementById("profile-link").addEventListener("click", LoadProfilePage);

document.getElementById("register-link").addEventListener("click", LoadRegisterPage);

document.getElementById("logout").addEventListener("click", Logout);

