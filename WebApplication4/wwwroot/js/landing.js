
const phoneNumbers = [
    "01222952593",
    "01198765432",
    "01255588899"
];

let index = 0;
const phoneSpan = document.getElementById("phone-number");

setInterval(() => {
    phoneSpan.style.transition = "opacity 0.5s ease-in-out";
    phoneSpan.style.opacity = 0;

    setTimeout(() => {
        phoneSpan.textContent = phoneNumbers[index];
        phoneSpan.style.opacity = 1;
        index = (index + 1) % phoneNumbers.length;
    }, 500);
}, 3000);


document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener("click", function(e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute("href"));
        if(target) {
            target.scrollIntoView({ behavior: "smooth", block: "start" });
        }
    });
});

const heroText = document.querySelector(".hero-content h1");
const phrases = ["Pharmacy Management System", "Smart Health Solutions", "Doctors & Pharmacies"];
let phraseIndex = 0;
let letterIndex = 0;
let typingSpeed = 120;

function typeEffect() {
    if (letterIndex <= phrases[phraseIndex].length) {
        heroText.textContent = phrases[phraseIndex].slice(0, letterIndex) + "|";
        letterIndex++;
        setTimeout(typeEffect, typingSpeed);
    } else {
        setTimeout(() => {
            eraseEffect();
        }, 2000);
    }
}

function eraseEffect() {
    if (letterIndex >= 0) {
        heroText.textContent = phrases[phraseIndex].slice(0, letterIndex) + "|";
        letterIndex--;
        setTimeout(eraseEffect, 60);
    } else {
        phraseIndex = (phraseIndex + 1) % phrases.length;
        setTimeout(typeEffect, 500);
    }
}

typeEffect();


// =========================
// Scroll Animations using AOS (Animate On Scroll)
// =========================
// 1. Include AOS library in _Layout.cshtml head:
// <link href="https://cdn.jsdelivr.net/npm/aos@2.3.4/dist/aos.css" rel="stylesheet">
// 2. Include AOS JS at bottom:
// <script src="https://cdn.jsdelivr.net/npm/aos@2.3.4/dist/aos.js"></script>

// Initialize AOS
if(typeof AOS !== "undefined"){
    AOS.init({
        duration: 1200,
        once: true,
        easing: 'ease-in-out',
    });
}


// =========================
// Floating Bubbles Background (Fancy Effect)
// =========================
const heroSection = document.querySelector(".hero");
for(let i=0; i<15; i++){
    const bubble = document.createElement("div");
    bubble.classList.add("bubble");
    bubble.style.left = `${Math.random()*100}%`;
    bubble.style.animationDuration = `${5 + Math.random()*5}s`;
    bubble.style.width = `${10 + Math.random()*20}px`;
    bubble.style.height = bubble.style.width;
    heroSection.appendChild(bubble);
}
