import { createParticles, animateParticles } from './Particles.js';

const canvas = document.getElementById("animated-background");
const ctx = canvas.getContext("2d");


function resizeCanvas() {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
}
resizeCanvas();
window.addEventListener("resize", resizeCanvas);

let particles = [];


animateParticles(ctx, particles, canvas.width, canvas.height);


function onKeyPress(e) {
    const sound = document.querySelector(`audio[data-key="${e.keyCode}"]`);
    const key = document.querySelector(`div[data-key="${e.keyCode}"]`);
    if (!key || !sound) return; 

    key.classList.add("playing");
    sound.currentTime = 0;
    sound.play();

    const keyRect = key.getBoundingClientRect();
    const keyX = keyRect.left + keyRect.width / 2; 
    const keyY = keyRect.top + keyRect.height / 1.2; 
    createParticles(particles, keyX, keyY); 
}

function onClick(e) {
    const key = e.target.closest(".key"); 
    if (!key) return; 

    const sound = document.querySelector(`audio[data-key="${key.dataset.key}"]`);
    if (!sound) return;

    key.classList.add("playing");
    sound.currentTime = 0;
    sound.play();

    const keyRect = key.getBoundingClientRect();
    const keyX = keyRect.left + keyRect.width / 2;
    const keyY = keyRect.top + keyRect.height / 1.2; 
    createParticles(particles, keyX, keyY); 
}


function removePlaying(e) {
    e.target.classList.remove("playing");
}

const keys = Array.from(document.querySelectorAll(".key"));

keys.forEach(key => {
    key.addEventListener("transitionend", removePlaying);
    key.addEventListener("click", onClick);
    console.log("hello: ", key);
});

window.addEventListener("keydown", onKeyPress);


