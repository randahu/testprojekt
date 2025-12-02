namespace ordination_test;
using shared.Model;

[TestClass]

public class PNTest
{
    
   private Laegemiddel CreateLm()
    {
        return new Laegemiddel("TestMedicin", 0.1, 0.2, 0.3, "Stk");
    }

   // TEST 1 : GivDosis
   
    [TestMethod]
    public void GivDosis_ReturnsTrue_WhenDateIsWithinPeriod()
    {
        // Arrange
        var lm = CreateLm();
        var pn = new PN(new DateTime(2025, 1, 1), new DateTime(2025, 1, 10), 2, lm);
        var dato = new Dato { dato = new DateTime(2025, 1, 5) };

        // Act
        bool result = pn.givDosis(dato);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, pn.getAntalGangeGivet());
    }

    [TestMethod]
    public void GivDosis_ReturnsFalse_WhenDateIsBeforeStart()
    {
        // Arrange
        var lm = CreateLm();
        var pn = new PN(new DateTime(2025, 1, 3), new DateTime(2025, 1, 10), 2, lm);
        var dato = new Dato { dato = new DateTime(2025, 1, 2) };

        // Act
        bool result = pn.givDosis(dato);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(0, pn.getAntalGangeGivet());
    }
    
    
    // TEST 2 : DoegnDosis

    [TestMethod]
    public void DoegnDosis_CalculatesCorrectly()
    {
        // Arrange
        var lm = CreateLm();
        var pn = new PN(new DateTime(2025, 1, 1), new DateTime(2025, 1, 3), 2, lm);

        pn.givDosis(new Dato { dato = new DateTime(2025, 1, 1) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 1, 2) });

        // Act
        double result = pn.doegnDosis();

        // Assert
        Assert.AreEqual(2, result);  
    }

    // TEST 3 : SamletDosis 
    
    [TestMethod]
    public void SamletDosis_ReturnsTotalAmountGiven()
    {
        // Arrange
        var lm = CreateLm();
        var pn = new PN(new DateTime(2025, 1, 1), new DateTime(2025, 1, 5), 3, lm);

        pn.givDosis(new Dato { dato = new DateTime(2025, 1, 1) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 1, 1) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 1, 3) });

        // Act
        double result = pn.samletDosis();

        // Assert
        Assert.AreEqual(9, result);
    }
    
    
    
    
}