using Microsoft.EntityFrameworkCore;
using Service;
using Data;
using shared.Model;

namespace ordination_test;

[TestClass]
public class DataServiceTest
{
    private DataService service = null!;
    private OrdinationContext context = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<OrdinationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new OrdinationContext(options);
        service = new DataService(context);
        service.SeedData();
    }

    
    // 1) OpretPN – gyldigt input
  
    [TestMethod]
    public void OpretPN_ValidInput_CreatesPN()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now, DateTime.Now.AddDays(2));

        var pns = service.GetPNs();

        Assert.IsTrue(pns.Any(p => p.antalEnheder == 2));
    }

    
    // 2) OpretPN – startdato > slutdato
   
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretPN_StartAfterEnd_ThrowsException()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now, DateTime.Now.AddDays(-1));
    }

    
    // 3) OpretPN – patient findes ikke
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretPN_InvalidPatient_ThrowsException()
    {
        var lm = service.GetLaegemidler().First();

        service.OpretPN(-999, lm.LaegemiddelId, 2, DateTime.Now, DateTime.Now.AddDays(1));
    }

    
    // 4) AnvendOrdination – gyldig dato
    
    [TestMethod]
    public void AnvendOrdination_Valid_ReturnsSuccessMessage()
    {
        var pn = service.GetPNs().First();

        // Vælg en gyldig dato
        var dato = new Dato { dato = pn.startDen };

        var result = service.AnvendOrdination(pn.OrdinationId, dato);

        Assert.AreEqual("Dosis registreret", result);
    }

    
    // 5) AnvendOrdination – dato udenfor periode
    
    [TestMethod]
    public void AnvendOrdination_InvalidDate_ReturnsError()
    {
        var pn = service.GetPNs().First();

        var dato = new Dato { dato = pn.startDen.AddYears(-1) }; // helt forkert dato

        var result = service.AnvendOrdination(pn.OrdinationId, dato);

        Assert.AreEqual("Dato uden for gyldighedsperiode", result);
    }

   
    // 6) AnvendOrdination – ordination findes ikke
   
    [TestMethod]
    public void AnvendOrdination_UnknownId_ReturnsNotFound()
    {
        var dato = new Dato { dato = DateTime.Now };

        var result = service.AnvendOrdination(-999, dato);

        Assert.AreEqual("Ordination ikke fundet", result);
    }
}
