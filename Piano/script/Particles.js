export class Particle {
    constructor(x, y) {
        this.x = x;
        this.y = y;
        this.radius = Math.random() * 5 + 2; 
        this.speedX = Math.random() * 2 - 1; 
        this.speedY = Math.random() * 2 - 1; 
        this.opacity = 1; 
    }

    draw(ctx) {
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
        ctx.fillStyle = `rgba(180, 160, 200, ${this.opacity})`; 
        ctx.shadowColor = 'rgba(255, 255, 255, 1)';
        ctx.shadowBlur = 15;
        ctx.fill();
    }

    
    update() {
        this.x += this.speedX;
        this.y += this.speedY;
        this.opacity -= 0.02; 
        if (this.opacity <= 0) {
            this.opacity = 0;
        }
    }
}


export function createParticles(particles, x, y) {
    for (let i = 0; i < 15; i++) { 
        particles.push(new Particle(x, y)); 
    }
}


export function animateParticles(ctx, particles, canvasWidth, canvasHeight) {
    ctx.clearRect(0, 0, canvasWidth, canvasHeight); 

    for (let i = 0; i < particles.length; i++) { 
        let particle = particles[i];

        if (particle.opacity <= 0) {
            particles.splice(i, 1);
            i--;
        } else {
            particle.update(); 
            particle.draw(ctx); 
        }
    }

    requestAnimationFrame(function() {
        animateParticles(ctx, particles, canvasWidth, canvasHeight); 
    });
}
