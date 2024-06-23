using RFTools.Calculator;
using RFTools.Constants;

namespace RFTools.Tests.xUnit;

public class CalculatorTests
{
    [Theory]
    [InlineData(0.000001678, 0.999991, 2_400_000_000, 1.3308)]
    [InlineData(0.000001678, 0.999991, 4_000_000_000, 1.0308)]
    [InlineData(0.000001678, 0.999991, 5_000_000_000, 0.922)]
    public void CalculateSkinDepth_ReturnResult_SameValue(
        double resistivity,
        double permeability,
        double frequency,
        decimal expected)
    {
        // Arrange
        var calculator = new SkinDepth();

        // Act
        var result = calculator.Calculate(resistivity, permeability, frequency);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void CalculateSkinDepth_ReturnResult_SameUnits()
    {
        // Arrange
        var calculator = new SkinDepth();

        // Act
        var result = calculator.Calculate(0.000001678, 0.999991, 2_400_000_000);

        // Assert
        Assert.Equal("µm", result.Unit);
    }

    [Theory]
    [InlineData(StandingWaves.VSWR, 5, 5, 0.67, 3.52, 2.55)]
    [InlineData(StandingWaves.ReflectionCoefficient, 0.5, 3, 0.5, 6.02, 1.25)]
    [InlineData(StandingWaves.ReturnLoss, 2, 8.72, 0.79, 2, 4.33)]
    public void CalculateStandingWave_ReturnResults_EqualResult(
        StandingWaves type,
        double input,
        decimal expectedVSWR,
        decimal expectedReflectionCoefficient,
        decimal expectedReturnLoss,
        decimal expectedMismatchLoss)
    {
        // Arrange
        var calculator = new StandingWave();

        // Act
        var result = calculator.Calculate(type, input, precision: 2);
        var actualVSWR = result.FirstOrDefault(o => o.Name.Equals("VSWR"))!.Value;
        var actualReflectionCoefficient = result.FirstOrDefault(o => o.Name.Contains("Reflection Coefficient"))!.Value;
        var actualReturnLoss = result.FirstOrDefault(o => o.Name.Equals("Return Loss"))!.Value;
        var actualMismatchLoss = result.FirstOrDefault(o => o.Name.Equals("Mismatch Loss"))!.Value;
       
        // Assert
        Assert.Equal(expectedVSWR, actualVSWR);
        Assert.Equal(expectedReflectionCoefficient, actualReflectionCoefficient);
        Assert.Equal(expectedReturnLoss, actualReturnLoss);
        Assert.Equal(expectedMismatchLoss, actualMismatchLoss);
    }

    [Fact]
    public void CalculateStandingWave_InvalidReflectionCoefficient_ThrowArgumentException()
    {
        // Arrange
        var calculator = new StandingWave();

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => calculator.Calculate(StandingWaves.ReflectionCoefficient, 1));
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.5)]
    public void CalculateStandingWave_InvalidVSWR_ThrowArgumentException(double input)
    {
        // Arrange
        var calculator = new StandingWave();
        
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => calculator.Calculate(StandingWaves.VSWR, input));
    }
}