document.addEventListener('DOMContentLoaded', function() {
    // Leichte Animation für Cards beim Laden
    const cards = document.querySelectorAll('.card');
    cards.forEach((card, index) => {
        setTimeout(() => {
            card.classList.add('card-visible');
        }, 100 * index);
    });

    // Checkbox-Funktionalität für Task-Liste
    const checkboxes = document.querySelectorAll('.task-list input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const taskText = this.nextElementSibling;
            if (this.checked) {
                taskText.classList.add('task-completed');
            } else {
                taskText.classList.remove('task-completed');
            }
        });
    });
    
    // Hover-Effekt für Karten (zusätzliche Details anzeigen)
    const kanbanCards = document.querySelectorAll('.card');
    kanbanCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            // Optional: Hier könnte zusätzliche Info angezeigt werden
            this.style.zIndex = "10";
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.zIndex = "1";
        });
    });
});