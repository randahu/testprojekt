namespace ordination_test;
using shared.Model;
[TestClass]


public class DagligSkævTest
{
    private Laegemiddel CreateLm()
    {
        return new Laegemiddel("TestMedicin", 0.1, 0.2, 0.3, "Stk");
    }

    [TestMethod]
    public void DoegnDosis_SumsAllDosesCorrectly()
    {
        // Arrange
        var lm = CreateLm();
        var ds = new DagligSkæv(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 5),
            lm
        );

        ds.doser.Add(new Dosis(new DateTime(2025, 1, 1, 9, 0, 0), 1));
        ds.doser.Add(new Dosis(new DateTime(2025, 1, 1, 12, 0, 0), 2));
        ds.doser.Add(new Dosis(new DateTime(2025, 1, 1, 18, 0, 0), 3));

        // Act
        double result = ds.doegnDosis();

        // Assert
        Assert.AreEqual(6, result);  // 1+2+3 = 6
    }

    [TestMethod]
    public void DoegnDosis_ReturnsZero_WhenNoDosesExist()
    {
        // Arrange
        var lm = CreateLm();
        var ds = new DagligSkæv(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 5),
            lm
        );

        // Act
        double result = ds.doegnDosis();

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void SamletDosis_CalculatesCorrectTotal()
    {
        // Arrange
        var lm = CreateLm();
        var ds = new DagligSkæv(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 3),  // 3 dage
            lm
        );

        ds.doser.Add(new Dosis(new DateTime(2025, 1, 1, 9, 0, 0), 2));
        ds.doser.Add(new Dosis(new DateTime(2025, 1, 1, 12, 0, 0), 2));

        // doegnDosis = 4
        // samletDosis = 4 * 3 = 12

        // Act
        double result = ds.samletDosis();

        // Assert
        Assert.AreEqual(12, result);
    }  
}