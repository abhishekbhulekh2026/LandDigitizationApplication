/******************************************************************************
 * PDF Viewer Component
 ******************************************************************************/

// const PdfViewer = {

//     initialize() {

//     },

//     load(pdfUrl) {

//     },

//     previousPage() {

//     },

//     nextPage() {

//     },

//     zoomIn() {

//     },

//     zoomOut() {

//     },

//     rotate() {

//     },

//     fitWidth() {

//     }

// };

const PdfViewer = {

    currentPage: 1,

    totalPages: 0,

    zoom: 100,

    init() {

        console.log("PDF Viewer Initialized");

    },

    load(url) {

        console.log("Loading PDF :", url);

    },

    nextPage() {

        this.currentPage++;

    },

    previousPage() {

        this.currentPage--;

    },

    zoomIn() {

        this.zoom += 10;

    },

    zoomOut() {

        this.zoom -= 10;

    }

};