const charBox = document.getElementById('doctor-ai');
const passInputs = document.querySelectorAll('.pass-input');
const pupils = document.querySelectorAll('.pupil');

// Eye Tracking
document.addEventListener('mousemove', (e) => {
    if (!charBox.classList.contains('hide-eyes')) {
        pupils.forEach(pupil => {
            const rect = pupil.getBoundingClientRect();
            const x = rect.left + rect.width / 2;
            const y = rect.top + rect.height / 2;
            const angle = Math.atan2(e.clientY - y, e.clientX - x);
            pupil.style.transform = `translate(${Math.cos(angle) * 4.5}px, ${Math.sin(angle) * 4.5}px)`;
        });
    }
});

// Hide eyes for both password fields
passInputs.forEach(input => {
    input.addEventListener('focus', () => {
        charBox.classList.add('hide-eyes');
        pupils.forEach(p => p.style.transform = 'translate(0,0)');
    });
    input.addEventListener('blur', () => charBox.classList.remove('hide-eyes'));
});