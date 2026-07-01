$(function () {

    const API_BASE_URL = "https://localhost:7087";

    async function login() {

        const userName = $("#txtUserName").val().trim();
        const password = $("#txtPassword").val();

        if (!userName || !password) {
            alert("Username and Password are required.");
            return;
        }

        try {

            const response = await fetch(`${API_BASE_URL}/api/Login/UserLogin`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    userName: userName,
                    password: password
                })
            });

            if (!response.ok) {
                throw new Error(`HTTP ${response.status}`);
            }

            const result = await response.json();

            if (result.ResponseMessage?.toLowerCase() === "success") {

                localStorage.setItem("jwtToken", result.Token);
                localStorage.setItem("userId", result.UserID);

                alert(localStorage.getItem("jwtToken"));
                window.location.href = "/Dashboard";


            } else {
                alert(result.ResponseMessage);
            }

        }
        catch (error) {
            console.error(error);
            alert("Unable to login.");
        }
    }

    $("#btnLogin").on("click", function (e) {
        e.preventDefault();
        login();
    });

});