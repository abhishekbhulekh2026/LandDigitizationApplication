$(function () {

    $("#btnLogin").click(login);

});

async function login() {

    const model = {

        userName: $("#txtUserName").val().trim(),

        password: $("#txtPassword").val()

    };

    try {

        Loader.show();

        const result = await Api.request(

            "/api/Login/UserLogin",

            {

                method: "POST",

                body: JSON.stringify(model)

            });

        Loader.hide();

        if (result.ResponseMessage !== "success") {

            Notify.error(result.ResponseMessage);

            return;

        }

        localStorage.setItem("jwtToken", result.Token);

        localStorage.setItem("userId", result.UserID);

        window.location.href = "/Index";

    }
    catch (e) {

        Loader.hide();

        Notify.error(e.message);

    }

}



// $(function () {

//     $("#btnLogin").click(login);

// });

// async function login() {

//     const loginData = {
//         userName: $("#txtUserName").val().trim(),
//         password: $("#txtPassword").val()
//     };

//     try {

//         const result = await Api.request("/api/Login/UserLogin", {
//             method: "POST",
//             body: JSON.stringify(loginData)
//         });

//         console.log(result);

//         if (result.ResponseMessage === "success") {

//             localStorage.setItem("jwtToken", result.Token);
//             localStorage.setItem("userId", result.UserID);

//             window.location.href = "/Index";

//         } else {

//             alert(result.ResponseMessage);

//         }

//     }
//     catch (e) {

//         console.error(e);
//         alert(e.message);

//     }

// }