use stgenetics

go




CREATE PROCEDURE FilterAnimals
    @AnimalId INT = NULL,
    @Name NVARCHAR(100) = NULL,
    @Sex NVARCHAR(10) = NULL,
    @Status NVARCHAR(10) = NULL
AS
BEGIN
    SELECT *
    FROM Animal
    WHERE (@AnimalId IS NULL OR AnimalId = @AnimalId)
        OR (@Name IS NULL OR Name = @Name)
        OR (@Sex IS NULL OR Sex = @Sex)
        OR (@Status IS NULL OR Status = @Status)
END
