using System;
using System.IO;
using System.Collections.Generic;

public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal AverageGrade { get; set; }
}

public class Program
{
    public static void Main()
    {
        // Путь к бинарному файлу с данными о студентах
        string filePath = "путь_к_файлу.bin";

        // Создание директории "Students" на рабочем столе
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string studentsDirectory = Path.Combine(desktopPath, "Students");
        Directory.CreateDirectory(studentsDirectory);

        // Чтение данных из бинарного файла и запись в текстовые файлы
        List<Student> students = ReadStudentsFromBinaryFile(filePath);
        WriteStudentsToTextFiles(students, studentsDirectory);
    }

    public static List<Student> ReadStudentsFromBinaryFile(string filePath)
    {
        List<Student> students = new List<Student>();

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.PeekChar() > -1)
            {
                Student student = new Student();
                student.Name = reader.ReadString();
                student.Group = reader.ReadString();
                student.DateOfBirth = DateTime.FromBinary(reader.ReadInt64());
                student.AverageGrade = reader.ReadDecimal();

                students.Add(student);
            }
        }

        return students;
    }

    public static void WriteStudentsToTextFiles(List<Student> students, string directoryPath)
    {
        foreach (Student student in students)
        {
            string groupFilePath = Path.Combine(directoryPath, $"{student.Group}.txt");

            using (StreamWriter writer = new StreamWriter(groupFilePath, true))
            {
                writer.WriteLine($"{student.Name}, {student.DateOfBirth}, {student.AverageGrade}");
            }
        }
    }
}