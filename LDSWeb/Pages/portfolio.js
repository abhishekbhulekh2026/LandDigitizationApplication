/******************************************************************************
 * Lucknow Bhulekh Portal
 * Welcome Page
 * Part 1
 ******************************************************************************/

const Welcome = {

    init() {

        this.cache();

        this.events();

        this.animateHero();

        this.initCounters();

        this.handleScroll();

    },

    cache() {

        this.backTop = document.getElementById("btnTop");

        this.header = document.querySelector(".portal-header");

        this.counters = document.querySelectorAll(".counter");

    },

    events() {

        window.addEventListener("scroll", () => {

            this.handleScroll();

        });

        if (this.backTop) {

            this.backTop.addEventListener("click", () => {

                window.scrollTo({

                    top: 0,

                    behavior: "smooth"

                });

            });

        }

        document.querySelectorAll('a[href^="#"]').forEach(link => {

            link.addEventListener("click", e => {

                const targetId = link.getAttribute("href");

                if (targetId === "#")
                    return;

                const target = document.querySelector(targetId);

                if (!target)
                    return;

                e.preventDefault();

                target.scrollIntoView({

                    behavior: "smooth",

                    block: "start"

                });

            });

        });

    },

    handleScroll() {

        const top = window.pageYOffset;

        if (this.header) {

            if (top > 40) {

                this.header.style.boxShadow =
                    "0 10px 30px rgba(0,0,0,.12)";

            }
            else {

                this.header.style.boxShadow =
                    "0 4px 12px rgba(0,0,0,.08)";

            }

        }

        if (this.backTop) {

            this.backTop.style.display =
                top > 300 ? "flex" : "none";

        }

    },

    animateHero() {

        const hero = document.querySelector(".hero-content");

        const card = document.querySelector(".dm-card");

        if (hero) {

            hero.animate([

                {

                    opacity: 0,

                    transform: "translateY(50px)"

                },

                {

                    opacity: 1,

                    transform: "translateY(0)"

                }

            ], {

                duration: 900,

                easing: "ease-out",

                fill: "forwards"

            });

        }

        if (card) {

            card.animate([

                {

                    opacity: 0,

                    transform: "translateY(40px) scale(.9)"

                },

                {

                    opacity: 1,

                    transform: "translateY(0) scale(1)"

                }

            ], {

                duration: 1000,

                delay: 250,

                easing: "ease-out",

                fill: "forwards"

            });

        }

    },

    initCounters() {

        const observer = new IntersectionObserver(entries => {

            entries.forEach(entry => {

                if (!entry.isIntersecting)
                    return;

                const counter = entry.target;

                this.animateCounter(counter);

                observer.unobserve(counter);

            });

        }, {

            threshold: .4

        });

        this.counters.forEach(c => observer.observe(c));

    },

    animateCounter(counter) {

        const target = parseInt(counter.dataset.target);

        const duration = 1800;

        const start = performance.now();

        const update = now => {

            const progress = Math.min(

                (now - start) / duration,

                1

            );

            const value = Math.floor(

                progress * target

            );

            counter.innerText = value.toLocaleString();

            if (progress < 1) {

                requestAnimationFrame(update);

            }

        };

        requestAnimationFrame(update);

    }

};

/******************************************************************************
 * Lucknow Bhulekh Portal
 * Welcome Page
 * Part 2
 ******************************************************************************/

Object.assign(Welcome, {

    initScrollReveal() {

        const elements = document.querySelectorAll(

            ".vision-card," +
            ".objective-card," +
            ".service-card," +
            ".benefit-card," +
            ".gallery-card," +
            ".workflow-box," +
            ".stat-card," +
            ".message-card," +
            ".profile-panel"

        );

        const observer = new IntersectionObserver(entries => {

            entries.forEach(entry => {

                if (!entry.isIntersecting)
                    return;

                entry.target.style.opacity = "1";

                entry.target.style.transform = "translateY(0)";

                observer.unobserve(entry.target);

            });

        }, {

            threshold: .15

        });

        elements.forEach(el => {

            el.style.opacity = "0";

            el.style.transform = "translateY(40px)";

            el.style.transition =
                "all .7s ease";

            observer.observe(el);

        });

    },

    initGallery() {

        document.querySelectorAll(".gallery-card img")

            .forEach(img => {

                img.addEventListener("mousemove", e => {

                    const rect = img.getBoundingClientRect();

                    const x =

                        ((e.clientX - rect.left) / rect.width - .5) * 10;

                    const y =

                        ((e.clientY - rect.top) / rect.height - .5) * 10;

                    img.style.transform =

                        `scale(1.08) rotateX(${-y}deg) rotateY(${x}deg)`;

                });

                img.addEventListener("mouseleave", () => {

                    img.style.transform = "";

                });

            });

    },

    highlightSection() {

        const sections = document.querySelectorAll("section");

        window.addEventListener("scroll", () => {

            const current = window.scrollY + 180;

            sections.forEach(sec => {

                if (

                    current >= sec.offsetTop &&

                    current < sec.offsetTop + sec.offsetHeight

                ) {

                    sec.classList.add("active-section");

                }
                else {

                    sec.classList.remove("active-section");

                }

            });

        });

    },

    parallaxHero() {

        const hero = document.querySelector(".hero-section");

        if (!hero)
            return;

        window.addEventListener("scroll", () => {

            hero.style.backgroundPositionY =

                -(window.scrollY * .18) + "px";

        });

    },

    initRipple() {

        document.querySelectorAll(".btn")

            .forEach(btn => {

                btn.addEventListener("click", function (e) {

                    const circle = document.createElement("span");

                    const d = Math.max(

                        this.clientWidth,

                        this.clientHeight

                    );

                    circle.style.width = d + "px";

                    circle.style.height = d + "px";

                    circle.style.position = "absolute";

                    circle.style.borderRadius = "50%";

                    circle.style.background =

                        "rgba(255,255,255,.45)";

                    circle.style.left =

                        (e.offsetX - d / 2) + "px";

                    circle.style.top =

                        (e.offsetY - d / 2) + "px";

                    circle.style.pointerEvents = "none";

                    circle.style.transform = "scale(0)";

                    circle.style.animation =

                        "ripple .6s ease-out";

                    this.appendChild(circle);

                    setTimeout(() => {

                        circle.remove();

                    }, 600);

                });

            });

    }

});

/* ---------------------------------------------------------- */
/* Extend Init                                                */
/* ---------------------------------------------------------- */

document.addEventListener("DOMContentLoaded", () => {

    Welcome.init();

    Welcome.initScrollReveal();

    Welcome.initGallery();

    Welcome.highlightSection();

    Welcome.parallaxHero();

    Welcome.initRipple();

});