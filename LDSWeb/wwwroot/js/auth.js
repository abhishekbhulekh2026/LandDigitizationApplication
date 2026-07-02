// function requireAuth() {

//     const token = localStorage.getItem("jwtToken");

//     if (!token) {
//         window.location.replace("/Login");
//         return false;
//     }

//     return true;
// }

const Auth = {

    logout() {

        localStorage.removeItem("jwtToken");

        localStorage.removeItem("userId");

        window.location.replace("/Login");

    },

    token() {

        return localStorage.getItem("jwtToken");

    },

    isLoggedIn() {

        return this.token() != null;

    }

};