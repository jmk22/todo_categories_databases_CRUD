using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ToDoList
{
  public class Task
  {
    private int id;
    private string description;
    private int categoryId;

    public Task(string Description, int CategoryId, int Id = 0)
    {
      id = Id;
      description = Description;
      categoryId = CategoryId;
    }

    public override bool Equals(System.Object otherTask)
    {
        if (!(otherTask is Task))
        {
          return false;
        }
        else {
          Task newTask = (Task) otherTask;
          bool idEquality = this.GetId() == newTask.GetId();
          bool descriptionEquality = this.GetDescription() == newTask.GetDescription();
          bool categoryEquality = this.GetCategoryId() == newTask.GetCategoryId();
          return (idEquality && descriptionEquality && categoryEquality);
        }
    }


    public int GetId()
    {
      return id;
    }
    public string GetDescription()
    {
      return description;
    }
    public void SetDescription(string newDescription)
    {
      description = newDescription;
    }
    public int GetCategoryId()
    {
      return categoryId;
    }
    public void SetCategoryId(int newCategoryId)
    {
      categoryId = newCategoryId;
    }
    public static List<Task> GetAll()
    {
      List<Task> AllTasks = new List<Task>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        int taskCategoryId = rdr.GetInt32(2);
        Task newTask = new Task(taskDescription, taskCategoryId, taskId);
        AllTasks.Add(newTask);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllTasks;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description, categoryId) OUTPUT INSERTED.id VALUES (@TaskDescription, @TaskCategoryId)", conn);

      SqlParameter descriptionParam = new SqlParameter();
      descriptionParam.ParameterName = "@TaskDescription";
      descriptionParam.Value = this.GetDescription();

      SqlParameter categoryIdParam = new SqlParameter();
      categoryIdParam.ParameterName = "@TaskCategoryId";
      categoryIdParam.Value = this.GetCategoryId();

      cmd.Parameters.Add(descriptionParam);
      cmd.Parameters.Add(categoryIdParam);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Task Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks WHERE id = @TaskId", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = id.ToString();
      cmd.Parameters.Add(taskIdParameter);
      rdr = cmd.ExecuteReader();

      int foundTaskId = 0;
      string foundTaskDescription = null;
      int foundTaskCategoryId = 0;

      while(rdr.Read())
      {
        foundTaskId = rdr.GetInt32(0);
        foundTaskDescription = rdr.GetString(1);
        foundTaskCategoryId = rdr.GetInt32(2);
      }
      Task foundTask = new Task(foundTaskDescription, foundTaskCategoryId, foundTaskId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundTask;
    }
  }
}
