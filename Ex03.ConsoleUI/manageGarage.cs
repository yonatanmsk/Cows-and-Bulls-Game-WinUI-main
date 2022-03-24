using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ManageGarage
    {
        public enum eOptionsForGarage
        {
            AddVehicleToGarage = 1,
            ShowLicenseNumberByChoiceOfUser,
            ChangeTheStatusOfVehicle,
            InflateWheelsOfVehicle,
            FillGasInTankInVehicle,
            ChargeTheEngineOfVehicle,
            ShowAllDetailsAboutVehcileByLicenseNumber,
            ByeBye,
        }

        private readonly Garage r_Garage = new Garage();
        private readonly UI r_UI = new UI();

        public Garage TheGarage
        {
            get
            {
                return r_Garage;
            }
        }

        public void Run()
        {
            eOptionsForGarage choiseOfUser;
            string openingWord = "Welcome", openingSentence = "Please enter which option do you want to do: ";
            string messageToUser = string.Format("{0}, {1}", openingWord, openingSentence);

            r_UI.PrintOutPutMessageForUser(messageToUser);
            choiseOfUser = r_UI.ChooseOptionFromListOfOptions<eOptionsForGarage>(GetOneOfTheOptionsToUser());
            Console.Clear();
            while(choiseOfUser != eOptionsForGarage.ByeBye)
            {
                switch(choiseOfUser)
                {
                    case eOptionsForGarage.AddVehicleToGarage:
                        AddVehicleToGarage();
                        break;

                    case eOptionsForGarage.ShowLicenseNumberByChoiceOfUser:
                        ShowLicenseNumberByChoiceGettingInputs();
                        break;
                        
                    case eOptionsForGarage.ChangeTheStatusOfVehicle:
                        ChangeConditionOfVehicleByLicenseNumber();
                        break;
                        
                    case eOptionsForGarage.InflateWheelsOfVehicle:
                        InflatetheWheelsOfSelectedVeheicle();
                        break;
                        
                    case eOptionsForGarage.FillGasInTankInVehicle:
                        FillGasInTankInVehicleByUser();
                        break;
                        
                    case eOptionsForGarage.ChargeTheEngineOfVehicle:
                        ChargeTheEngineOfVehicleByUser();
                        break;
                        
                    case eOptionsForGarage.ShowAllDetailsAboutVehcileByLicenseNumber:
                        ShowAllDetailsOfVehicleByLicense();
                        break;
                }

                Console.WriteLine();
                Console.WriteLine();
                r_UI.PrintOutPutMessageForUser(openingSentence);
                choiseOfUser = r_UI.ChooseOptionFromListOfOptions<eOptionsForGarage>(Enum.GetNames(typeof(eOptionsForGarage)));
                Console.Clear();
            }
        }

        public string[] GetOneOfTheOptionsToUser()
        {
            return Enum.GetNames(typeof(eOptionsForGarage));
        }

        public void AddVehicleToGarage()
        {
            Vehicle vehicle;
            StoredVehicleInGarage findSpecificVehicle, newVehicleToInsert;
            string licenseNumber, ownerName, ownerPhoneNumber, messageForUser = "Congratz! You inserted succefully a vehicle to the garage!";

            licenseNumber = r_UI.GetLicenseNumberFromUser();
            findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumber));
            if(findSpecificVehicle != null)
            {
                r_UI.PrintVehicleExist();
                r_Garage.ChangeConditionInGarage(findSpecificVehicle, StoredVehicleInGarage.eConditionInGarage.InRepair);
            }
            else
            {
                vehicle = CreateNewVehicle(licenseNumber);
                FillMustDetailsAboutVehicle(vehicle);
                FillSpecificDetailsAboutVehicle(vehicle);
                r_UI.GetInformationOfOwner(out ownerName, out ownerPhoneNumber);
                newVehicleToInsert = new StoredVehicleInGarage(ownerName, ownerPhoneNumber, StoredVehicleInGarage.eConditionInGarage.InRepair, vehicle);
                r_Garage.GarageOfVehicles.Add(newVehicleToInsert);
                r_UI.PrintOutPutMessageForUser(messageForUser);
            }
        }

        public void FillSpecificDetailsAboutVehicle(Vehicle i_NewVehicle)
        {
            if(i_NewVehicle is Car)
            {
                r_UI.DefineDetailsOfCar(i_NewVehicle, Car.DefineNumberOFCarDoors(), Car.DefineNumberOFCarColor());
            }
            else if(i_NewVehicle is Motorcycle)
            {
                r_UI.DefineDetailsOfMotorCycle(i_NewVehicle, Motorcycle.DefineLicenseKind());
            }
            else if(i_NewVehicle is Truck)
            {
                r_UI.DefineDetailsOfTruck(i_NewVehicle);
            }
        }

        public void FillMustDetailsAboutVehicle(Vehicle i_NewVehicle)
        {
            string nameOfManufacturOfWheels;
            float exactlyAirPressure;
            bool checkIfEnergyAmountIsValid = false, checkIfAirPressureIsValid = false;

            i_NewVehicle.ModelOfVehicle = r_UI.VehicleModelDefine();
            while(!checkIfAirPressureIsValid)
            {
                try
                {
                    r_UI.GetDetailsAboutWheels(out nameOfManufacturOfWheels, out exactlyAirPressure);
                    i_NewVehicle.WriteDetailsOfWheels(nameOfManufacturOfWheels, exactlyAirPressure);
                    checkIfAirPressureIsValid = true;
                }
                catch(ValueOutOfRangeException exceptionOfInvalidation)
                {
                    r_UI.ErrorRangePrintMessage(exceptionOfInvalidation);
                }
            }

            while(!checkIfEnergyAmountIsValid)
            {
                try
                {
                    i_NewVehicle.FillEnergyAtEngine(r_UI.DefineTheActualEnergy());
                    checkIfEnergyAmountIsValid = true;
                }
                catch(ValueOutOfRangeException exceptionOfInvalidation)
                {
                    r_UI.ErrorRangePrintMessage(exceptionOfInvalidation);
                }
            }
        }

        private Vehicle CreateNewVehicle(string i_LicenseNumber)
        {
            Vehicle newVehicle;
            FactoryOfVehicles.eTypeOfVehicle typeOfVehicle;

            typeOfVehicle = r_UI.GetVehicleTypeChoice(FactoryOfVehicles.GetVehicleOptions());
            newVehicle = FactoryOfVehicles.CreateVehicle(i_LicenseNumber, typeOfVehicle);
            newVehicle.CreateEngine(r_Garage.DefineSourceOfEnergyByVehicleType(typeOfVehicle));
            newVehicle.InitializeWheels();

            return newVehicle;
        }
        
        public void ChangeConditionOfVehicleByLicenseNumber()
        {
            StoredVehicleInGarage.eConditionInGarage choiceOfCondition;
            string licenseNumberToChange = r_UI.GetLicenseNumberFromUser();
            string messageForUseToChangeCondition = "Insert new Condition for the selected license: ";

            r_UI.PrintOutPutMessageForUser(messageForUseToChangeCondition);
            choiceOfCondition = r_UI.ChooseOptionFromListOfOptions<StoredVehicleInGarage.eConditionInGarage>(Enum.GetNames(typeof(StoredVehicleInGarage.eConditionInGarage)));
            StoredVehicleInGarage findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumberToChange));
            try
            {
                if(findSpecificVehicle != null)
                {
                    findSpecificVehicle.VehicleCondition = choiceOfCondition;
                    r_UI.PrintOutPutMessageForUser("The status of the vehicle has changed!");
                }
                else
                {
                    r_UI.PrintOutPutMessageForUser("There is no such vehicle to change condition");
                }
            }
            catch(ArgumentException ex)
            {
                r_UI.PrintOutPutMessageForUser(ex.Message);
            }
        }

        public void ShowLicenseNumberByChoiceGettingInputs()
        {
            string messageToUser = "Please enter which kind of vehicles you want to see by their condition (InRepair, Fixed, Paid): ", temporaryChoice;
            r_UI.PrintOutPutMessageForUser(messageToUser);
            string[] conditionThatUserChoose = new string[3];

            for(int i = 0; i < 3; i++)
            {
                conditionThatUserChoose[i] = string.Empty;
            }

            temporaryChoice = r_UI.GetInputFromUser();
            for(int i = 0; i < 3; i++)
            {
                if(temporaryChoice == "0")
                {
                    break;
                }
                
                conditionThatUserChoose[i] = temporaryChoice;
                r_UI.PrintOutPutMessageForUser("If you want just this condition press 0, otherwise continue: ");
                temporaryChoice = r_UI.GetInputFromUser();
            }

            ShowLicenseNumberByChoice(conditionThatUserChoose[0], conditionThatUserChoose[1], conditionThatUserChoose[2]);
        }

        public void ShowLicenseNumberByChoice(string i_FirstOptionalConditionToShow = "", string i_SecondOptionalConditionToShow = "", string i_ThirdOptionalConditionToShow = "")
        {
            if(i_FirstOptionalConditionToShow != string.Empty)
            {
                ShowLicenseNumberByChoice(i_FirstOptionalConditionToShow);
            }

            if(i_SecondOptionalConditionToShow != string.Empty)
            {
                ShowLicenseNumberByChoice(i_SecondOptionalConditionToShow);
            }

            if(i_ThirdOptionalConditionToShow != string.Empty)
            {
                ShowLicenseNumberByChoice(i_ThirdOptionalConditionToShow);
            }
        }

        public void ShowLicenseNumberByChoice(string i_OptionalConditionToShow)
        {
            string firstMessageOfConditionVehicle = string.Format("The license numbers of condition {0} are: ", i_OptionalConditionToShow);
            r_UI.PrintOutPutMessageForUser(firstMessageOfConditionVehicle);

            foreach(StoredVehicleInGarage vehicle in r_Garage.GarageOfVehicles)
            {
                if(vehicle.VehicleCondition.ToString() == i_OptionalConditionToShow)
                {
                    r_UI.PrintOutPutMessageForUser(vehicle.VehicleInGarage.LicenseNumber);
                }
            }
        }

        public void InflatetheWheelsOfSelectedVeheicle()
        {
            string licenseNumberFromUser = r_UI.GetLicenseNumberFromUser();
            StoredVehicleInGarage findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumberFromUser));

            if(findSpecificVehicle != null)
            {
                for(int i = 0; i < findSpecificVehicle.VehicleInGarage.SetWheels.Length; i++)
                {
                    findSpecificVehicle.VehicleInGarage.SetWheels[i].FillAirPressureInWheels
                        (findSpecificVehicle.VehicleInGarage.SetWheels[i].MaxAirPressureOfWheel
                        - findSpecificVehicle.VehicleInGarage.SetWheels[i].CurrentAirPressure);
                }
            }
        }

        public void FillGasInTankInVehicleByUser()
        {
            float amountOfFuelToAdd;
            string licenseNumberFromUser = r_UI.GetLicenseNumberFromUser();
            GasolineEngine.eGasolineType gasolineType = r_UI.ChooseOptionFromListOfOptions<GasolineEngine.eGasolineType>(GasolineEngine.ShowGasolineTypes());
            amountOfFuelToAdd = r_UI.GetAmountOfEnergyToAdd("gasoline");
            StoredVehicleInGarage findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumberFromUser));

            try
            {
                r_Garage.FillGasolineInVehicle(findSpecificVehicle, gasolineType, amountOfFuelToAdd);
            }
            catch(ValueOutOfRangeException exceptionOfRange)
            {
                r_UI.ErrorRangePrintMessage(exceptionOfRange);
            }
            catch(Exception exception)
            {
                r_UI.PrintOutPutMessageForUser(exception.Message);
            }
        }

        public void ChargeTheEngineOfVehicleByUser()
        {
            string licenseNumberFromUser = r_UI.GetLicenseNumberFromUser();
            StoredVehicleInGarage findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumberFromUser));
            float amountOfTimeToAddToEngine = r_UI.GetAmountOfEnergyToAdd("time");

            try
            {
                r_Garage.FillTimeBatteryInVehicle(findSpecificVehicle, amountOfTimeToAddToEngine);
            }
            catch(ValueOutOfRangeException exceptionRange)
            {
                r_UI.ErrorRangePrintMessage(exceptionRange);
            }
            catch(Exception exception)
            {
                r_UI.PrintOutPutMessageForUser(exception.Message);
            }
        }

        private void ShowAllDetailsOfVehicleByLicense()
        {
            string licenseNumberFromUser = r_UI.GetLicenseNumberFromUser();
            StoredVehicleInGarage findSpecificVehicle = r_Garage.GarageOfVehicles.Find(x => x.VehicleInGarage.LicenseNumber.Contains(licenseNumberFromUser));

            try
            {
                r_UI.PrintOutPutMessageForUser(r_Garage.TakeInformationOfClientFromGarage(findSpecificVehicle));
            }
            catch(Exception exception)
            {
                r_UI.PrintOutPutMessageForUser(exception.Message);
            }
        }
    }
}