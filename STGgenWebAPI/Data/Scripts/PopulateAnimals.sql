use STGenetics ;
DECLARE @Counter INT = 1;
delete Animal;


WHILE @Counter <= 5000
BEGIN
  DECLARE @Name VARCHAR(50);
  DECLARE @Breed VARCHAR(50);
  DECLARE @BirthDate DATE;
  DECLARE @Sex VARCHAR(10);
  DECLARE @Price DECIMAL(10, 2);
  DECLARE @Status VARCHAR(10);
  DECLARE @CowBreeds TABLE (Breed VARCHAR(50));


 
	INSERT INTO @CowBreeds (Breed)
	VALUES ('Angus'), ('Jersey'), ('Holstein'), ('Hereford'), ('Simmental'), ('Limousin'), ('Charolais'), ('Gelbvieh');

	SELECT TOP 1 @Breed = Breed
	FROM @CowBreeds
	ORDER BY NEWID();
	SET @Sex = CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Male' ELSE 'Female' END;
 
  SET @BirthDate =  DATEADD(MONTH, -1 * (ABS(CHECKSUM(NEWID())) % 120 + 11), GETDATE());
 
 
 SET @Sex = CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Male' ELSE 'Female' END;
  
  
  DECLARE @FirstName VARCHAR(50);
    DECLARE @LastName VARCHAR(50);

  IF @Sex = 'Male'
  BEGIN
 
 
    SET @FirstName = (
        SELECT TOP 1 Name
        FROM (VALUES ('Ferdinand'), ('Bodacious'), ('Bushwacker'), ('Red Rock'), ('Little Yellow Jacket'), 
        ('Asteroid'), ('Code Blue'), ('Chicken on a Chain'), ('Blueberry Wine'), ('Pistol'), 
        ('White Sports Coat'), ('Panhandle Slim'), ('Troubadour'), ('Black Pearl'), ('Long John'), 
        ('Super Freak'), ('Blackberry Wine'), ('Pearl Harbor'), ('Cowtown Classic'), ('Dirty Money'), 
        ('Lunatic'), ('Tuff Hedeman'), ('Mr. T'), ('Hard Copy'), ('Gunslinger'), 
        ('Warrior'), ('Widow Maker'), ('Major Payne'), ('Pit Boss'), ('Copperhead')) AS FirstNameTable(Name)
        ORDER BY NEWID()
    );

   


  END
  ELSE
  BEGIN


    SET @FirstName = (
        SELECT TOP 1 Name
        FROM (VALUES ('Daisy'), ('Bella'), ('Luna'), ('Molly'), ('Rosie'), 
        ('Lily'), ('Coco'), ('Ruby'), ('Sadie'), ('Mia'), 
        ('Stella'), ('Zoe'), ('Lucy'), ('Sophie'), ('Lola'), 
        ('Chloe'), ('Penny'), ('Abby'), ('Gracie'), ('Maddie'), 
        ('Bailey'), ('Emma'), ('Hazel'), ('Nala'), ('Olive'), 
        ('Piper'), ('Willow'), ('Zara'), ('Cali'), ('Gigi')) AS FirstNameTable(Name)
        ORDER BY NEWID()
    );

    
 
 END
  SET @LastName = (
        SELECT TOP 1 Name
        FROM (VALUES  ('Wrangler'), ('Buckhorn'), ('Stampede'), ('Thunder'), ('Raging Bull'), 
        ('Wilder'), ('Bronco'), ('Hornblower'), ('Outlaw'), ('Maverick'), 
        ('Trailblazer'), ('Bullrider'), ('Tornado'), ('Rodeo'), ('Bullseye'), 
        ('Stallion'), ('Bullhorn'), ('Desperado'), ('Champion'), ('Rustler'), 
        ('Spurlock'), ('Ironside'), ('Hooves'), ('Hornet'), ('Bullard'), 
        ('Lonestar'), ('Blazing Saddle'), ('Roughrider'), ('Longhorn'), 
        ('Rodeoman'), ('Wrangler'), ('Bucking Bronco'), ('Bullseye'), 
        ('Bullwhip'), ('Stampeder'), ('Wilder'), ('Bullrider'), ('Stallion'), 
        ('Wrangler'), ('Bronco'), ('Rodeo'), ('Outlaw'), ('Tornado'), 
        ('Bullseye'), ('Thunder'), ('Maverick'), ('Trailblazer'), 
        ('Raging Bull'), ('Buckhorn'), ('Rustler'), ('Hooves')) AS LastNameTable(Name)
        ORDER BY NEWID()
    );
  SET @Name =  @FirstName + ' ' + @LastName


   SET @Price = ROUND((ABS(CHECKSUM(NEWID())) % 49501) + 500, 2);

  SET @Status = CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Active' ELSE 'Inactive' END;
 

 DECLARE @id int 
 
   select @id  = isnull ( max(AnimalId) , 0 ) + 1 from Animal


  INSERT INTO Animal (AnimalId, Name, Breed, BirthDate, Sex, Price, Status)
  VALUES (@id ,@Name, @Breed, @BirthDate, @Sex, @Price, @Status);

  SET @Counter = @Counter + 1;
END;
