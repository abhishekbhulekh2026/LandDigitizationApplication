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

            alert(result.ResponseMessage);

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

