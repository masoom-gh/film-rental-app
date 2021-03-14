
export const LoadRegisterPageContent = async () => {
    document.querySelector("#main-content").innerHTML = "";
    const response = await fetch("./register.html");
    const data = await response.text();
    document.querySelector("#main-content").innerHTML = data;

    document.getElementById("reg-form").addEventListener("submit", (e) => {
        if (e.preventDefault) e.preventDefault();
        Register();
        return false;
    });
}


export const LoadRegisterPage = async (e) => {
    try {
        e.preventDefault();
        await LoadRegisterPageContent();
    } catch (e) {
        console.log(e, "Unable to load partial html.");
    }
}



const Register = async () => {
    try {
        const errorMessage = document.getElementById("reg-error");
        const successMessage = document.getElementById("reg-success-message");

        let usernameValue = document.getElementById("reg-username").value;
        let passwordValue = document.getElementById("reg-password").value;
        let emailValue = document.getElementById("reg-email").value;
        let studioNameValue = document.getElementById("reg-studio-name").value;
        let cityValue = document.getElementById("reg-city-name").value;
        let chairmanNameValue = document.getElementById("reg-chairman-name").value;
        let chairmanMobileValue = document.getElementById("reg-chiarman-mobile").value;

        const values = {
            username: usernameValue,
            Password: passwordValue,
            email: emailValue,
            studioName: studioNameValue,
            city: cityValue,
            chairmanName: chairmanNameValue,
            chairmanMobileNumber: chairmanMobileValue
        };


        const response = await fetch("api/authentication/register",
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });



        if (response.status === 200) {
            errorMessage.innerHTML = "";
            ResetRegisterInputs();
            successMessage.innerHTML = "Registeration was successful!";
        }
        else if (response.status === 500) {
            successMessage.innerHTML = "";
            ResetRegisterInputs();
            errorMessage.innerHTML = "A server error occured. Probably, this username has already been registered. Try to sign up with another username.";
        }
        else if (response.status === 400) {
            successMessage.innerHTML = "";
            ResetRegisterInputs();
            const data = await response.json();
            errorMessage.innerHTML = data.title;
        }


    } catch (e) {
        console.log(e);
    }


}

const ResetRegisterInputs = () => {
    document.getElementById("reg-username").value = "";
    document.getElementById("reg-password").value = "";
    document.getElementById("reg-email").value = "";
    document.getElementById("reg-studio-name").value = "";
    document.getElementById("reg-city-name").value = "";
    document.getElementById("reg-chairman-name").value = "";
    document.getElementById("reg-chiarman-mobile").value = "";
}