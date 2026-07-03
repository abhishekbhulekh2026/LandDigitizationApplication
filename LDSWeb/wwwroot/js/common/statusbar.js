/******************************************************************************
 * Status Bar Component
 ******************************************************************************/

// const StatusBar = {

//     initialize() {

//     },

//     setMessage(message) {

//     },

//     setPage(pageNo) {

//     },

//     setTotalRecords(total) {

//     },

//     setUser(userName) {

//     },

//     setProgress(value) {

//     }

// };

const StatusBar = {

    setStatus(message) {

        $("#lblSaveStatus")
            .text(message);

    },

    setPage(pageNo) {

        $("#lblStatusPage")
            .text(pageNo);

    },

    setRecordCount(count) {

        $("#lblRecordCount")
            .text(count);

    }

};