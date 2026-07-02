// const Api = {

//     baseUrl: "https://localhost:7087",

//     async request(url, options = {}) {

//         const token = localStorage.getItem("jwtToken");

//         options.headers = {
//             "Content-Type": "application/json",
//             ...(options.headers || {}),
//             ...(token ? { "Authorization": `Bearer ${token}` } : {})
//         };

//         const response = await fetch(this.baseUrl + url, options);

//         if (response.status === 401) {
//             localStorage.clear();
//             window.location.href = "/Login";
//             return null;
//         }

//         if (!response.ok) {
//             let message = `HTTP ${response.status}`;

//             try {
//                 const error = await response.json();
//                 message = error.responseMessage || error.message || message;
//             } catch {
//                 message = await response.text();
//             }

//             throw new Error(message);
//         }

//         return response.json();
//     }
// };

const Api = {

    baseUrl: "https://localhost:7087",

    async request(url, options = {}) {

        const token = localStorage.getItem("jwtToken");

        options = {

            method: "GET",

            headers: {},

            ...options

        };

        options.headers = {

            "Content-Type": "application/json",

            ...(token && {

                Authorization: `Bearer ${token}`

            }),

            ...options.headers

        };

        try {

            const response = await fetch(this.baseUrl + url, options);

            if (response.status === 401) {

                Auth.logout();

                return null;

            }

            if (!response.ok) {

                let message = "Server Error";

                try {

                    const error = await response.json();

                    message =
                        error.responseMessage ??
                        error.message ??
                        message;

                }
                catch {

                    message = await response.text();

                }

                throw new Error(message);

            }

            return await response.json();

        }
        catch (e) {

            console.error(e);

            throw e;

        }

    }

};