namespace shared.Model;

public class PN : Ordination {
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN(DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) 
        : base(laegemiddel, startDen, slutDen) {
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// </summary>
    public bool givDosis(Dato givesDen) {
        // Implementeret Tjekker om dato er inden for gyldighedsperiode og husker dosis
        if (givesDen == null) return false;
        var datoOnly = givesDen.dato.Date;
        var start = startDen.Date;
        var slut = slutDen.Date;
        if (datoOnly < start || datoOnly > slut) return false;

        dates.Add(new Dato { dato = givesDen.dato }); // Gemmer dato
        return true;
    }

    public override double doegnDosis() {
        // Implementeret Beregner døgndosis som total givet dosis / antal dage
        if (dates == null || dates.Count == 0) return 0;
        var minDate = dates.Min(d => d.dato.Date);
        var maxDate = dates.Max(d => d.dato.Date);
        var dage = (maxDate - minDate).Days + 1;
        if (dage <= 0) dage = 1; // sikkerhed
        double totalGivet = antalEnheder * dates.Count;
        return totalGivet / dage;
    }

    public override double samletDosis() {
        return dates.Count * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count;
    }

    public override String getType() {
        return "PN";
    }
}