if (!window.languageSelectorInitialized) {

    document.body.addEventListener('submit', (event) => {
        const form = event.target;
        const returnUrlInput = form && form.querySelector('.language-return-url');
        if (returnUrlInput) {
            returnUrlInput.value = window.location.pathname + window.location.search;
        }
    }, true);

    window.languageSelectorInitialized = true;
}
