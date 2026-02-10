async function askQuestion() {
    const input = document.getElementById('question');
    const chatWindow = document.getElementById('chat-window');
    const question = input.value.trim();

    if (!question) return;


    appendMessage(question, 'user');
    input.value = '';


    const loadingId = addLoadingIndicator();

    try {
        const response = await fetch('/Chatbot/Ask', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Question: question })
        });

        if (!response.ok) throw new Error();

        const data = await response.json();

        removeLoadingIndicator(loadingId);
        appendMessage(data.answer, 'ai');

    } catch (error) {
        removeLoadingIndicator(loadingId);
        appendMessage("عذراً دكتور، حدث خطأ أثناء معالجة الطلب. يرجى المحاولة مرة أخرى.", 'ai');
    }
}

function appendMessage(text, sender) {
    const chatWindow = document.getElementById('chat-window');
    const msgDiv = document.createElement('div');
    const time = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    msgDiv.className = `message ${sender}`;
    const icon = sender === 'ai' ? '<i class="fas fa-robot" style="margin-left: 8px; color: var(--accent-color);"></i> ' : '';

    msgDiv.innerHTML = `${icon}${text} <br><small style="font-size: 0.7rem; opacity: 0.6; display: block; margin-top: 8px;">${time}</small>`;

    chatWindow.appendChild(msgDiv);
    chatWindow.scrollTop = chatWindow.scrollHeight;
}

function addLoadingIndicator() {
    const id = 'loading-' + Date.now();
    const chatWindow = document.getElementById('chat-window');
    const loadDiv = document.createElement('div');
    loadDiv.className = 'message ai';
    loadDiv.id = id;
    loadDiv.innerHTML = `<i class="fas fa-sync fa-spin" style="margin-left: 8px;"></i> جاري تحليل البيانات الطبية...`;
    chatWindow.appendChild(loadDiv);
    chatWindow.scrollTop = chatWindow.scrollHeight;
    return id;
}

function removeLoadingIndicator(id) {
    const el = document.getElementById(id);
    if (el) el.remove();
}


document.getElementById('question').addEventListener('keypress', function (e) {
    if (e.key === 'Enter') {
        askQuestion();
    }
});