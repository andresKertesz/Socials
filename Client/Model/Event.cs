namespace Socials.Client.Client.Model
{
    public class Event
    {
        public long Id { get; set; }
        public long DueñoId { get; set; }
        public long? VisibilidadEventoId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEvento { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public long Like { get; set; }
        public long Asistentes { get; set; }
        public bool Habilitado { get; set; }

        public Event(
        long id, long dueñoId, long? visibilidadEventoId, string nombre, string descripcion,
        DateTime fechaCreacion, DateTime fechaEvento, decimal longitud, decimal latitud,
        long like, long asistentes, bool habilitado)
        {
            Id = id;
            DueñoId = dueñoId;
            VisibilidadEventoId = visibilidadEventoId;
            Nombre = nombre;
            Descripcion = descripcion + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            FechaCreacion = fechaCreacion;
            FechaEvento = fechaEvento;
            Longitud = longitud;
            Latitud = latitud;
            Like = like;
            Asistentes = asistentes;
            Habilitado = habilitado;
        }

    }
    public static class EventData
    {
        public static List<Event> GetFixedEvents()
        {
            return new List<Event>
        {
            new Event(
                1, 101, 201, "Concierto en el Parque", "Disfruta de una tarde musical en el parque...",
                DateTime.Parse("2023-08-28T10:00:00Z"), DateTime.Parse("2023-09-15T15:30:00Z"), -73.987654m, 40.712345m, 50, 200, true),
            new Event(
                2, 102, 202, "Charla de Innovación Tecnológica", "Explora las últimas tendencias en tecnología...",
                DateTime.Parse("2023-08-29T14:00:00Z"), DateTime.Parse("2023-09-20T18:00:00Z"), -74.012345m, 40.721234m, 30, 150, true),
            new Event(
                3, 103, 201, "Feria Gastronómica Internacional", "Embárcate en un viaje culinario alrededor del mundo...",
                DateTime.Parse("2023-08-30T09:30:00Z"), DateTime.Parse("2023-09-25T12:45:00Z"), -73.987123m, 40.705432m, 25, 100, true),
            new Event(
                4, 104, 201, "Taller de Arte en la Naturaleza", "Despierta tu creatividad mientras pintas al aire libre...",
                DateTime.Parse("2023-09-01T11:15:00Z"), DateTime.Parse("2023-09-30T10:00:00Z"), -73.998765m, 40.709876m, 40, 120, true),
            new Event(
                5, 105, 202, "Partido Benéfico de Fútbol", "Apoya una causa noble mientras disfrutas de un emocionante partido...",
                DateTime.Parse("2023-09-03T16:00:00Z"), DateTime.Parse("2023-10-10T14:30:00Z"), -73.995432m, 40.718901m, 60, 180, true),
            new Event(
                6, 106, 201, "Exposición de Fotografía Urbana", "Descubre la esencia de la ciudad a través de la lente de talentosos fotógrafos...",
                DateTime.Parse("2023-09-05T14:30:00Z"), DateTime.Parse("2023-10-15T11:00:00Z"), -74.003210m, 40.724567m, 35, 130, true),
            new Event(
                7, 107, 202, "Festival de Cine al Aire Libre", "Disfruta de noches de películas bajo las estrellas con una selección de películas...",
                DateTime.Parse("2023-09-08T18:00:00Z"), DateTime.Parse("2023-10-20T19:15:00Z"), -74.008765m, 40.719876m, 55, 220, true),
            new Event(
                8, 108, 201, "Conferencia de Salud Mental", "Obtén información valiosa sobre la salud mental y el bienestar en esta conferencia...",
                DateTime.Parse("2023-09-10T09:00:00Z"), DateTime.Parse("2023-10-25T13:30:00Z"), -73.988765m, 40.707654m, 20, 90, true),
        };
        }

    }
}
