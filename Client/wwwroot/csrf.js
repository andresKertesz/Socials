window.getCsrfToken = function () {
    // Retrieve the CSRF token from a meta tag with the name "csrf-token"
    var csrfTokenMeta = document.querySelector('meta[name="csrf-token"]');
    if (csrfTokenMeta) {
        return csrfTokenMeta.getAttribute('content');
    } else {
        console.error('CSRF token meta tag not found.');
        return null;
    }
}