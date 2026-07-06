/*
==========================================================
Viewer.js
Enterprise Document Viewer
Land Digitization System
==========================================================
*/

const Viewer = {

    pages: [],

    currentIndex: 0,

    zoom: 100,

    rotation: 0,

    fitMode: "width",

    imageElement: null,

    initialize() {

        this.imageElement = document.getElementById("viewerImage");

        console.log("Viewer Initialized");

    },

    loadPages(pageList) {

        this.pages = pageList || [];

        this.currentIndex = 0;

        if (this.pages.length > 0) {

            this.loadCurrentPage();

        }

    },

    loadCurrentPage() {

        if (this.pages.length === 0)
            return;

        const page = this.pages[this.currentIndex];

        this.load(page);

    },

    load(page) {

        if (!page)
            return;

        StatusBar.setStatus("Loading scanned record...");

        $("#viewerPlaceholder").hide();

        $("#viewerImage").show();

        const url = page.fileUrl;      // <-- Use this for now change here const url = page.viewerUrl;

        this.imageElement.src = url;

        this.imageElement.onload = () => {

            StatusBar.setStatus("Ready");

            this.applyTransform();

            this.preloadNext();

            this.updatePageInfo();

        };

        this.imageElement.onerror = () => {

            StatusBar.setStatus("Unable to load scanned record.");

            Notification.error("Image could not be loaded.");

            this.clear();

        };
    },


    next() {

        if (this.currentIndex >= this.pages.length - 1)
            return;

        this.currentIndex++;

        this.loadCurrentPage();

    },

    previous() {

        if (this.currentIndex <= 0)
            return;

        this.currentIndex--;

        this.loadCurrentPage();

    },

    goto(pageNumber) {

        const index =
            this.pages.findIndex(x => x.pageNumber === pageNumber);

        if (index >= 0) {

            this.currentIndex = index;

            this.loadCurrentPage();

        }

    },

    zoomIn() {

        this.zoom += 10;

        this.applyTransform();

    },

    zoomOut() {

        if (this.zoom <= 20)
            return;

        this.zoom -= 10;

        this.applyTransform();

    },

    fitWidth() {

        this.fitMode = "width";

        this.imageElement.style.width = "100%";

        this.imageElement.style.height = "auto";

    },

    fitHeight() {

        this.fitMode = "height";

        this.imageElement.style.height = "100%";

        this.imageElement.style.width = "auto";

    },

    rotateRight() {

        this.rotation += 90;

        this.applyTransform();

    },

    rotateLeft() {

        this.rotation -= 90;

        this.applyTransform();

    },

    reset() {

        this.zoom = 100;

        this.rotation = 0;

        this.applyTransform();

    },

    applyTransform() {

        this.imageElement.style.transform =
            `scale(${this.zoom / 100}) rotate(${this.rotation}deg)`;

    },

    preloadNext() {

        if (this.currentIndex >= this.pages.length - 1)
            return;

        const img = new Image();

        img.src =
            this.pages[this.currentIndex + 1].fileUrl;

    },

    currentPage() {

        return this.pages[this.currentIndex];

    },

    totalPages() {

        return this.pages.length;

    },
    clear() {

        this.imageElement.src = "";

        $("#viewerImage").hide();

        $("#viewerPlaceholder").show();

    },
    updatePageInfo() {

        $("#lblCurrentPage").text(
            `Page ${this.currentIndex + 1} / ${this.pages.length}`
        );

    }
};