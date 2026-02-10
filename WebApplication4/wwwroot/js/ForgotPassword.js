const pupils = document.querySelectorAll('.pupil');


document.addEventListener('mousemove', (e) => {
    pupils.forEach(pupil => {
        const rect = pupil.getBoundingClientRect();
        const x = rect.left + rect.width / 2;
        const y = rect.top + rect.height / 2;
        const angle = Math.atan2(e.clientY - y, e.clientX - x);
        const distance = 4.5;
        pupil.style.transform = `translate(${Math.cos(angle) * distance}px, ${Math.sin(angle) * distance}px)`;
    });
});