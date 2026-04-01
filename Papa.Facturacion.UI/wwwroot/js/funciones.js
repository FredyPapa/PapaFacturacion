window.CarouselFunctions = {
    UpdateCarouselPosition: function (index) {
        var slides = document.querySelectorAll('.carousel-slide');
        slides.forEach((slide, i) => {
            slide.style.display = (i === index) ? 'block' : 'none';
        });
    }
};

// Initialize first slide display
document.addEventListener('DOMContentLoaded', () => {
    window.CarouselFunctions.UpdateCarouselPosition(0);
});

window.descargarArchivo = (nombre, contenido, formato) => {
    const blob = new Blob([new Uint8Array(contenido)], { type: formato });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = nombre;
    a.click();
    URL.revokeObjectURL(url);
}