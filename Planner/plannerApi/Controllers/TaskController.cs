using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace plannerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly plannerApi.Models.taskContext _context;

        public TaskController(plannerApi.Models.taskContext context) => _context = context;
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public class PaginatedTodo
    {
        public List<TodoItem>? Items { get; set; }
        public int TotalCount { get; set; }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class TodoPageService
    {
        private readonly HttpClient _httpClient;

        public TodoPageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync(string searchString, int page = 1, int pageSize = 10)
        {
            var endpoint = !string.IsNullOrEmpty(searchString)
                ? $"api/TodoItems?search={searchString}&page={page}&pageSize={pageSize}"
                : $"api/TodoItems?page={page}&pageSize={pageSize}";

            using var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API call failed with status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<PaginatedTodo>(content);

            return results?.Items ?? new List<TodoItem>();
        }
    }
}
