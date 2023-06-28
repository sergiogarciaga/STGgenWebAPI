USE STGenetics;
GO

CREATE PROCEDURE DeleteAnimal
  @AnimalId INT
AS
BEGIN
  DELETE FROM Animal
  WHERE AnimalId = @AnimalId;
END;
