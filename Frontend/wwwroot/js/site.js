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

    fetch('/api/projekte')
        .then(response => {
            if (!response.ok) throw new Error("Fehler beim Laden der Projekte");
            return response.json();
        })
        .then(projekte => {
            const listContainer = document.querySelector('.task-list');
            listContainer.innerHTML = ''; // Alte Tasks entfernen

            projekte.forEach(projekt => {
                const li = document.createElement('li');

                const checkbox = document.createElement('input');
                checkbox.type = 'checkbox';

                const spanName = document.createElement('span');
                spanName.textContent = projekt.name;

                const spanDate = document.createElement('span');
                spanDate.className = 'date';
                spanDate.textContent = 'API-Daten'; // Später anpassbar

                li.appendChild(checkbox);
                li.appendChild(spanName);
                li.appendChild(spanDate);

                listContainer.appendChild(li);
            });
        })
        .catch(error => {
            console.error("Fehler beim Abrufen der Projekte:", error);
        });

    document.getElementById('projekt-form').addEventListener('submit', function (e) {
        e.preventDefault();
        const name = document.getElementById('projekt-name').value;

        fetch('/api/projekte', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: name,
                kundeId: 1, // Dummy
                dauerGeschaetztStunden: 0,
                dauerBerechnetStunden: 0,
                beschreibung: "Neues Projekt",
                status: "ToDo",
                startdatum: new Date().toISOString(),
                enddatum: new Date().toISOString()
            })
        })
            .then(res => {
                if (!res.ok) throw new Error("Fehler beim Erstellen");
                return res.json();
            })
            .then(() => location.reload()); // oder dynamisch hinzufügen
    });

});