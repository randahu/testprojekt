namespace ordination_test;
using shared.Model;
[TestClass]

public class DagligFastTest
{
    private Laegemiddel CreateLm()
    {
        // opretter et test-laegemiddel til brug i alle tests 
        return new Laegemiddel("TestMedicin", 0.1, 0.2, 0.3, "Stk");
    }

    // Tester om doegndosis() lægger moregn+middag+aften+nat korret sammen 
    [TestMethod]
    public void DoegnDosis_CalculatesCorrectSum()
    {
        // Arrange
        var lm = CreateLm();
        var df = new DagligFast(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 3),
            lm,
            2, 1, 3, 0   // morgen, middag, aften, nat
        );

        // Act
        double result = df.doegnDosis();

        // Assert
        Assert.AreEqual(6, result);  // 2+1+3+0 = 6
    }

    
    // tester om samletDosis() ganger døgndosis med antal dage 
    [TestMethod]
    public void SamletDosis_CalculatesCorrectTotal()
    {
        // Arrange
        var lm = CreateLm();
        var df = new DagligFast(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 3),  // 3 dage (1, 2, 3)
            lm,
            1, 1, 1, 1   // døgnsum = 4
        );

        // Act
        double result = df.samletDosis();

        // Assert
        Assert.AreEqual(12, result); // 3 dage * 4 enheder = 12
    }

    
    // Tester at doegndosis() returnerer 0, hvis aller doser er 0 
    [TestMethod]
    public void DoegnDosis_ReturnsZero_WhenAllDosesAreZero()
    {
        // Arrange
        var lm = CreateLm();
        var df = new DagligFast(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 2),
            lm,
            0, 0, 0, 0
        );

        // Act
        double result = df.doegnDosis();

        // Assert
        Assert.AreEqual(0, result);
    }
}