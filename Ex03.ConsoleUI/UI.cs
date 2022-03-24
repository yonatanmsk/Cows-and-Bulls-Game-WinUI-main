using System;
using System.Text;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public string GetLicenseNumberFromUser()
        {
            Console.WriteLine("Please enter license number of the car: ");

            return GetStringFromUser();
        }

        public string GetStringFromUser()
        {
            string inputFromUser = Console.ReadLine();

            while(inputFromUser == string.Empty)
            {
                Console.WriteLine("Input is invalid. Please insert a value: ");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser;
        }

        public void PrintVehicleExist()
        {
            Console.WriteLine("The vehicle exist in the Garage. We will change its condition to in repairing mode");
        }

        public void GetInformationOfOwner(out string o_NameOfOwner, out string o_PhoneNumberOfOwner)
        {
            int phoneNumber;

            Console.WriteLine("Please enter the name of the vehicle's owner: ");
            o_NameOfOwner = GetStringFromUser();
            Console.WriteLine("Please enter the phone number of the vehicle's owner (numbers only): ");
            o_PhoneNumberOfOwner = Console.ReadLine();
            while(!int.TryParse(o_PhoneNumberOfOwner, out phoneNumber))
            {
                Console.WriteLine("Number phone is not valid, Please try again!");
                o_PhoneNumberOfOwner = Console.ReadLine();
            }
        }

        public FactoryOfVehicles.eTypeOfVehicle GetVehicleTypeChoice(string[] i_VehicleTypes)
        {
            Console.WriteLine("Please choose a vehicle type:");

            return ChooseOptionFromListOfOptions<FactoryOfVehicles.eTypeOfVehicle>(i_VehicleTypes);
        }

        private T GetChoiceValue<T>(string i_TheChoiceOfUser)
        {
            int actualNumberChoice;
            T theValueOfChoiceOfUser;

            if(int.TryParse(i_TheChoiceOfUser, out actualNumberChoice) && Enum.IsDefined(typeof(T), actualNumberChoice))
            {
                theValueOfChoiceOfUser = (T)Enum.Parse(typeof(T), i_TheChoiceOfUser);
            }
            else
            {
                throw new FormatException("The insertion that you did is incorrect, please insert option from the options!");
            }

            return theValueOfChoiceOfUser;
        }

        public string VehicleModelDefine()
        {
            Console.WriteLine("Please enter the vehicle model:");

            return GetStringFromUser();
        }

        public float DefineTheActualEnergy()
        {
            string currentEneryExist;
            float actualEnergyAmount;

            Console.WriteLine("Please enter exactly how much energy amount you have: ");
            currentEneryExist = Console.ReadLine();
            while(!float.TryParse(currentEneryExist, out actualEnergyAmount))
            {
                Console.WriteLine("The amount of energy you entered is invalid. Try again!");
                currentEneryExist = Console.ReadLine();
            }

            return actualEnergyAmount;
        }

        public void ErrorRangePrintMessage(ValueOutOfRangeException i_ExceptionOfInvalidation)
        {
            string alertOfInvalidationInput = string.Format("{0} {1} - {2}", i_ExceptionOfInvalidation.Message, i_ExceptionOfInvalidation.MinValue.ToString(), i_ExceptionOfInvalidation.MaxValue.ToString());
            Console.WriteLine(alertOfInvalidationInput);
        }

        public void GetDetailsAboutWheels(out string o_NameOfManufacturOfWheels, out float o_ExactlyAirPressure)
        {
            string currentAirPressure;

            Console.WriteLine("Plaese enter the manufacturer of the wheels: ");
            o_NameOfManufacturOfWheels = GetStringFromUser();
            Console.WriteLine("Plaese enter the current air pressure of the wheels: ");
            currentAirPressure = Console.ReadLine();
            while(!float.TryParse(currentAirPressure, out o_ExactlyAirPressure))
            {
                Console.WriteLine("Invalid wheel's air pressure, Try again");
                currentAirPressure = Console.ReadLine();
            }
        }

        public void DefineDetailsOfCar(Vehicle i_NewVehicle, string[] i_DoorsNumber, string[] i_CarColor)
        {
            Console.WriteLine("Please insert the number of doors: ");
            ((Car)i_NewVehicle).DoorsNumber = ChooseOptionFromListOfOptions<Car.eDoorsNumber>(i_DoorsNumber);
            Console.WriteLine("Please insert the car color: ");
            ((Car)i_NewVehicle).Color = ChooseOptionFromListOfOptions<Car.eColor>(i_CarColor);
        }

        public void DefineDetailsOfMotorCycle(Vehicle i_NewVehicle, string[] i_KindOfLicenses)
        {
            string engineVolume;
            int actualEngineVolume;

            Console.WriteLine("Please enter the volume of the engine: ");
            engineVolume = Console.ReadLine();
            while(!int.TryParse(engineVolume, out actualEngineVolume))
            {
                Console.WriteLine("You entered an invalid volume of engine. Please try again!");
                engineVolume = Console.ReadLine();
            }

            ((Motorcycle)i_NewVehicle).VolumeEngine = actualEngineVolume;
            Console.WriteLine("Please insert the license kind: ");
            ((Motorcycle)i_NewVehicle).LicenseKind = ChooseOptionFromListOfOptions<Motorcycle.eLicenseKind>(i_KindOfLicenses);
        }

        public void DefineDetailsOfTruck(Vehicle i_NewVehicle)
        {
            string isCooledTransportingContents, transportingContentsVolume;
            float actualTransportingContentsVolume;

            Console.WriteLine("Please insert the transporting Contents volume:");
            transportingContentsVolume = Console.ReadLine();
            while(!float.TryParse(transportingContentsVolume, out actualTransportingContentsVolume))
            {
                Console.WriteLine("You entered an invalid transporting Contents volume. Please try again!");
                transportingContentsVolume = Console.ReadLine();
            }

            Console.WriteLine("Please enter if the truck's transporting Contents is cooled? The answers is Yes or No: ");
            isCooledTransportingContents = Console.ReadLine();
            while(isCooledTransportingContents != "Yes" && isCooledTransportingContents != "No")
            {
                Console.WriteLine("The answer is Invalid, Please enter Yes or No: ");
                isCooledTransportingContents = Console.ReadLine();
            }

             ((Truck)i_NewVehicle).TrunkTransportingContentsVolume = actualTransportingContentsVolume;
            if(isCooledTransportingContents == "Yes")
            {
                ((Truck)i_NewVehicle).TransportingRefrigeratedContents = true;
            }
            else
            {
                ((Truck)i_NewVehicle).TransportingRefrigeratedContents = false;
            }
        }

        public void PrintOutPutMessageForUser(string i_MessageToPrintForUser)
        {
            Console.WriteLine(i_MessageToPrintForUser);
        }

        public string GetInputFromUser()
        {
            return Console.ReadLine();
        }

        public float GetAmountOfEnergyToAdd(string i_KindOfEnergy)
        {
            string energyInputByUser;
            float actualEnergyToAddToVehicle;

            Console.WriteLine("Please insert what kind of {0} to add: ", i_KindOfEnergy);
            energyInputByUser = Console.ReadLine();
            while(!float.TryParse(energyInputByUser, out actualEnergyToAddToVehicle))
            {
                Console.WriteLine("The amount you entered is not valid. Please try again!");
                energyInputByUser = Console.ReadLine();
            }

            return actualEnergyToAddToVehicle;
        }

        public T ChooseOptionFromListOfOptions<T>(string[] i_SetOfOptions)
        {
            int savingTheTemporaryIndex = 1;
            string theChoiceOfUser;
            bool isTheInputIsRight = false;
            T theOptionsFromSetT = default(T);

            foreach(string option in i_SetOfOptions)
            {
                Console.WriteLine("{0}. {1}", savingTheTemporaryIndex++, CorrectShowOfStringToUser(option));
            }

            theChoiceOfUser = Console.ReadLine();
            while(!isTheInputIsRight)
            {
                try
                {
                    theOptionsFromSetT = GetChoiceValue<T>(theChoiceOfUser);
                    isTheInputIsRight = true;
                }
                catch (FormatException exceptionOfFormatting)
                {
                    Console.WriteLine(exceptionOfFormatting.Message);
                    theChoiceOfUser = Console.ReadLine();
                }
            }

            return theOptionsFromSetT;
        }

        public string CorrectShowOfStringToUser(string i_SetOfEnumsOptions)
        {
            StringBuilder correctShowOfOption = new StringBuilder(i_SetOfEnumsOptions.Length);

            for(int i = 0; i < i_SetOfEnumsOptions.Length; i++)
            {
                if(char.IsUpper(i_SetOfEnumsOptions[i]))
                {
                    correctShowOfOption.Append(' ');
                }

                correctShowOfOption.Append(i_SetOfEnumsOptions[i]);
            }

            return correctShowOfOption.ToString();
        }
    }
}