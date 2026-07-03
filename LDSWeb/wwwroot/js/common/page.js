/******************************************************************************
 * Page Manager
 * Common page initialization
 ******************************************************************************/

const Page = {

    init() {

        this.bindEvents();

        console.log("Page Initialized");

    },

    bindEvents() {

        $(window).on("resize", () => {

            console.log("Window resized");

        });

    }

};