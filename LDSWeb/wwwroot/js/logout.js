$(function () {

    $("#btnLogout").on("click", async function (e) {

        e.preventDefault();

        await logout();

    });

});

async function logout() {

    try {

        await Api.request("/api/Login/Logout", {
            method: "POST"
        });

    } catch (err) {

        console.error(err);

    } finally {

        localStorage.removeItem("jwtToken");
        localStorage.removeItem("userId");

        window.location.href = "/Login";

    }
}