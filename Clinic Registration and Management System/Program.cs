using Clinic_Registration_and_Management_System.Model;
using Clinic_Registration_and_Management_System.MyDbContext;

namespace Clinic_Registration_and_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using ApplicationDnContext _db = new ApplicationDnContext();
            Console.WriteLine("Welcome to the Clinic Registration and Management System!");
             
            int userWant;
            while (true)
            {
                do
                {
                    Console.WriteLine("\n1.Patients\n" +
                        "2.Admin\n" +
                        "3.Exit\n" 
                        );
                    Console.Write("Enter Your choise:");
                } while (!int.TryParse(Console.ReadLine(), out userWant) && userWant != 3);

                if (userWant == 3) break;
                else if (userWant == 1) {
                    Patient patient = Patient_Details();
                    _db.Patients.Add(patient);
                    //_db.SaveChanges();


                    Appointment appointment = Determine_Appointment(patient, _db);
                    _db.Appointments.Add(appointment);
                    _db.SaveChanges();
                    Console.WriteLine("Your Appointment has Registered\n\n\t An Email will be send for Confirmation.");
                }
                else if (userWant == 2)
                {
                    int userWanted;
                    do
                    {
                        Console.WriteLine("\n1.View Appointments \n" +
                            "2.View and set Status of Appointments\n"
                            );
                        Console.Write("Enter Your choise:");
                    } while (!int.TryParse(Console.ReadLine(), out userWanted) && userWanted == 1 || userWanted == 2  );

                    if (userWant == 1) Just_View_Appointment(_db);
                    if (userWant == 2) View_And_Set_Status_Appointment(_db);
                }




            }
            Console.WriteLine("\nThank you for using the Minimalist Zoo Animal Management System!");


        }

        public static Patient Patient_Details()
        {
            Console.Write("\n\nEnter Your First Name:");
            string fname = Console.ReadLine();

            Console.Write("Enter Your Last Name:");
            string lname = Console.ReadLine();

            int age;

            do
            {
              Console.Write("Enter Your Age:");

            } while (!int.TryParse(Console.ReadLine(), out age) && age >= 0);

            Console.Write("Enter Your Email:");
            string email = Console.ReadLine();

            Console.Write("Enter Your Phone:");
            string phone = Console.ReadLine();

            Patient patient = new Patient() { FName = fname ,LName = lname, 
            Age = age, Email = email, Phone = phone};

            return  patient;
        }

        public static Appointment Determine_Appointment(Patient patient, ApplicationDnContext _db)
        {
            Console.WriteLine("\n");
            var Spec = (from S in _db.Specializations
                        select S).ToList();


            foreach (var item in Spec)
            {
                Console.Write((item?.SpecializationID ?? 0) + "  || ");
                Console.Write(item?.SpecializationName + "  ||  " ?? "NA  ||  ");
                Console.WriteLine(item?.Description ?? "NA");
                Console.WriteLine();
            }

            int Specialization;
            do
            {
                Console.Write("Where do you whant to treating:(1-4) ");
            } while (!int.TryParse(Console.ReadLine(), out Specialization) && Specialization > 0 && Specialization <= Spec.Count());

            Console.WriteLine("\nDetermine your Appointment:");

            Console.Write("Enter Day: Exp YYYY-MM-DD ");
            string AppointmentDay = Console.ReadLine();


            Console.Write("Enter Time: Exp HH:MI ");
            string AppointmentTime = Console.ReadLine();

            int Patient_ID = (from P in _db.Patients
                              where P.FName == patient.FName
                              select P.PatientID).FirstOrDefault();

            Appointment appointment = new Appointment()
            {
                Appointment_Day = AppointmentDay,
                Appointment_Time = AppointmentTime,
                Appointment_Status = "pending",
                PatientID = Patient_ID,
                SpecializationID = Specialization - 1
            };
            //set The patient
            appointment.Patient = patient;

            //set The Specialization
            var Specializationa = (from S in _db.Specializations
                              where S.SpecializationID == Specialization
                                   select S).FirstOrDefault();
            appointment.Specialization = Specializationa;

            return appointment;

            

        }

        public static void Just_View_Appointment(ApplicationDnContext _db)
        {
            Console.WriteLine("\n");
            var Apo = (from A in _db.Appointments
                       select A).ToList();


            foreach (var item in Apo)
            {
                Console.Write((item?.AppointmentID ?? 0) + "  || ");
                Console.Write(item?.Appointment_Day + "  ||  " ?? "NA  ||  ");
                Console.WriteLine(item?.Appointment_Time ?? "NA  ||  ");
                Console.Write(item?.Appointment_Status + "  ||  " ?? "NA  ||  ");
                Console.Write((item?.PatientID ?? 0) + "  || ");
                Console.Write(item?.Patient.FName + "  ||  " ?? "NA  ||  ");
                Console.Write(item?.Specialization.SpecializationName + "  ||  " ?? "NA  ||  ");
                Console.WriteLine();

                Console.Write("Set the Status of the Appointment:( Approve, Reschedule, or Cancel )");
                string status = Console.ReadLine();
                item.Appointment_Status = status;
                _db.Appointments.Update(item);
                _db.SaveChanges();
            }
        }
        public static void View_And_Set_Status_Appointment(ApplicationDnContext _db)
        {
            Console.WriteLine("\n");
            var Apo = (from A in _db.Appointments
                        select A).ToList();


            foreach (var item in Apo)
            {
                Console.Write((item?.AppointmentID ?? 0) + "  || ");
                Console.Write(item?.Appointment_Day + "  ||  " ?? "NA  ||  ");
                Console.WriteLine(item?.Appointment_Time ?? "NA  ||  ");
                Console.Write(item?.Appointment_Status + "  ||  " ?? "NA  ||  ");
                Console.Write((item?.PatientID ?? 0) + "  || ");
                Console.Write(item?.Patient.FName + "  ||  " ?? "NA  ||  ");
                Console.Write(item?.Specialization.SpecializationName + "  ||  " ?? "NA  ||  ");
                Console.WriteLine();

                Console.Write("Set the Status of the Appointment:( Approve, Reschedule, or Cancel )");
                string status = Console.ReadLine();
                item.Appointment_Status = status;
                _db.Appointments.Update(item);
                _db.SaveChanges();
            }

            Console.Write("All Appointment Status are Seted!");

        }
    }
}