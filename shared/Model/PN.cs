namespace shared.Model;

public class PN : Ordination {
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen) {
        if (givesDen == null) return false;
        var datoOnly = givesDen.dato.Date;
        var start = startDen.Date;
        var slut = slutDen.Date;
        if (datoOnly < start || datoOnly > slut) {
            return false;
        }

        // Husk datoen — gem en ny Dato-instans (vi tillader flere givninger samme dag)
        dates.Add(new Dato { dato = givesDen.dato });
        return true;
    }

    public override double doegnDosis() {
        // Hvis ingen doser er givet returneres 0
        if (dates == null || dates.Count == 0) return 0;

        // Find først- og sidstdato for givninger (kun dato-del)
        var minDate = dates.Min(d => d.dato.Date);
        var maxDate = dates.Max(d => d.dato.Date);

        var dage = (maxDate - minDate).Days + 1;
        if (dage <= 0) dage = 1; // sikkerhed

        double totalGivet = antalEnheder * dates.Count;
        double doegnDosis = totalGivet / (double)dage;
        return doegnDosis;
    }

    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

    public override String getType() {
        return "PN";
    }
}