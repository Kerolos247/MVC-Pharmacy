console.log("🔥 PRO LEVEL Login page loaded!");


document.querySelectorAll('.form-control').forEach(input => {
    input.addEventListener('focus', () => {
        input.style.background = 'rgba(25,135,84,0.08)';
    });
    input.addEventListener('blur', () => {
        input.style.background = 'white';
    });
});

document.querySelectorAll('.btn-login, .btn-outline-primary').forEach(btn => {
    btn.addEventListener('click', e => {
        const ripple = document.createElement('span');
        ripple.classList.add('ripple');
        ripple.style.left = e.offsetX + 'px';
        ripple.style.top = e.offsetY + 'px';
        btn.appendChild(ripple);
        setTimeout(() => {
            ripple.remove();
        }, 600);
    });
});
