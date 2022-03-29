
declare @PlotId int;
declare @HolderId int = 14;
declare @isHolderAnOwner bit  = (SELECT [IsHolderAnOwner] from 
								[Holding].[tHolder] 
								WHERE HolderId=@HolderId)
if(@isHolderAnOwner = 1)
	BEGIN
		SET @PlotId = (SELECT PlotId from [Holding].[tHolder] WHERE HolderId=14)
		-- update statement for holder id
		SELECT * FROM [Holding].[tHolderFlat] WHERE IsActive = 1 
		AND HolderFlatId IN (
							SELECT DISTINCT [HolderFlatId] FROM [Holding].[tHolderFlat]
							WHERE MainHolderId IN (
									SELECT DISTINCT HolderId FROM Holding.tHolder 
									WHERE PlotId = @PlotId))
		
		-- update statement for holder is checked 
				--if holderid = mainholderid and isactive = 1 then false
				-- if holderid != mainholderid and isactive = 1 then true
		
		-- update isHolderAnOwner false here
			SELECT HolderId from [Holding].[tHolder] 
			WHERE PlotId=@PlotId AND HolderId <> @HolderId
		
	END
ELSE
	print 'No'


