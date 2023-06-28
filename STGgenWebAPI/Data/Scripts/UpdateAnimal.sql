USE STGenetics;
GO

CREATE PROCEDURE UpdateAnimal
  @AnimalId INT,
  @Name VARCHAR(50),
  @Breed VARCHAR(50),
  @BirthDate DATE,
  @Sex VARCHAR(10),
  @Price DECIMAL(10, 2),
  @Status VARCHAR(10)
AS
BEGIN
  UPDATE Animal
  SET Name = @Name,
      Breed = @Breed,
      BirthDate = @BirthDate,
      Sex = @Sex,
      Price = @Price,
      Status = @Status
  WHERE AnimalId = @AnimalId;
END;
