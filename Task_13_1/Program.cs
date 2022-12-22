using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task_13_1
{
	abstract class Product
	{
		static public string[] productType = new string[] { "Игрушки", "Книги", "Спорт-Инвентарь" };

		static public void GetAllProductsInfo(Product[] product)
		{
			for (int i = 0; i < product.Length; i++)
			{
				product[i].GetProductInfo();
			}
		}

		abstract public void GetProductInfo();
		abstract public bool MatchingTheType(string type);
	}

	class Toy : Product
		{
			private string toyName;
			private float toyPrice;
			private string toyManufact;
			private string toyMaterial;
			private int toyDesignedForAge;

			public Toy(string toyName, float toyPrice, string toyManufact, string toyMaterial, int toyDesignedForAge)
			{
				this.toyName = toyName;
				this.toyPrice = toyPrice;
				this.toyManufact = toyManufact;
				this.toyMaterial = toyMaterial;
				this.toyDesignedForAge = toyDesignedForAge;
			}

			override public void GetProductInfo()
			{
				Console.WriteLine("\nТип товара: {0}\n" +
					"Наименование товара: {1}\n" +
					"Цена товара: {2}\n" +
					"Производитель товара: {3}\n" +
					"Материал товара: {4}\n" +
					"{5}\n",
					"Игрушка", toyName, toyPrice <= 0 ? "Бесплатно" : toyPrice, toyManufact, toyMaterial, 
					(toyDesignedForAge <= 0 ? "Товар рассчитан для всех возростных лиц" : "Возрастная категория: " + toyDesignedForAge + "+"));
			}

			override public bool MatchingTheType(string type)
			{
				if (type == Product.productType[0])
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

	class Book : Product
		{
			private string bookName;
			private string bookAuthor;
			private float bookPrice;
			private string bookPublicher;
			private int bookDesignedForAge;

			public Book(string bookName, string bookAuthor, float bookPrice, string bookPublicher, int bookDesignedForAge)
			{
				this.bookName = bookName;
				this.bookAuthor = bookAuthor;
				this.bookPrice = bookPrice;
				this.bookPublicher = bookPublicher;
				this.bookDesignedForAge = bookDesignedForAge;
			}

			override public void GetProductInfo()
			{
				Console.WriteLine("\nТип товара: {0}\n" +
					"Название книги: {1}" +
					"Автор книги: {2}\n" +
					"Цена книги: {3}\n" +
					"Издательство книги: {4}\n" +
					"{5}" + "\n",
					"Книга", bookName, bookAuthor, bookPrice <= 0 ? "Бесплатно" : bookPrice, bookPublicher, 
					(bookDesignedForAge <= 0 ? "Товар рассчитан для всех возростных лиц" : "Возрастная категория: " + bookDesignedForAge + "+"));
			}

			override public bool MatchingTheType(string type)
			{
				if (type == Product.productType[1])
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

	class SportsEquipment : Product
	{
		private string sEquipName;
		private float sEquipPrice;
		private string sEquipManufactur;
		private int sEquipDesignedForAge;

		public SportsEquipment(string sEquipName, float sEquipPrice, string sEquipManufactur, int sEquipDesignedForAge)
		{
			this.sEquipName = sEquipName;
			this.sEquipPrice = sEquipPrice;
			this.sEquipManufactur = sEquipManufactur;
			this.sEquipDesignedForAge = sEquipDesignedForAge;
		}

		override public void GetProductInfo()
		{
			Console.WriteLine("\nТип товара: {0}\n" +
				"Название спорт-инвентаря: {1}" +
				"Цена спорт-ивентаря: {2}\n" +
				"Производитель спорт-инвентаря: {3}\n" +
				"{4}\n",
				"Спорт-Инвентарь", sEquipName, sEquipPrice <= 0 ? "Бесплатно" : sEquipPrice, sEquipManufactur, 
				(sEquipDesignedForAge <= 0 ? "Товар рассчитан для всех возростных лиц" : "Возрастная категория: " + sEquipDesignedForAge + "+"));
		}

		override public bool MatchingTheType(string type)
		{
			if (type == Product.productType[2])
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	class Program 
	{
		static string[] ReadFile(FileStream fs)
		{
			byte[] buffer = new byte[fs.Length];
			fs.Read(buffer, 0, buffer.Length);
			string textFromFile = Encoding.Default.GetString(buffer);
			string[] textSplit = textFromFile.Split("\n");

			/*for (int i = 0; i < textSplit.Length; i++)
			{
				if (textSplit[i] == "\n")
				{
					List<string> temp = new List<string>(textSplit);
					temp.RemoveAt(i);
					textSplit = temp.ToArray();

					i = i == 1 ? i : i--;
				}
			}*/

			for (int i = 0; i < textSplit.Length; i++)
			{
				textSplit[i] = textSplit[i].Trim();
			}

			return textSplit;
		}

		static Product[] SetProductsFromFile()
		{
			Product[]? products = null;
			FileStream fileProducts;
			string filePath;

			Console.WriteLine("ПРИМЕЧАНИЕ: Файл должен иметь следующую структуру\n" +
				"\n1 - Если товар является Игрушкой, то должен иметь следующие пункты:\n\tТип (Игрушки), Наименование игрушки, Цена игрушки, Производитель игрушки, Материал игрушки, Возрастная категория игрушки\n" +
				"\n2 - Если товар является Книгой, то должен иметь следующие пункты:\n\tТип (Книги), Наименование книги, Автор книги, Цена книги, Издтельство книги, Возрастная категория книги\n" +
				"3 - Если товар является Спорт-Инвентарем, то должен иметь следующие пункты:\n\tТип (Спорт-Инвентарь), Наименование Спорт-Инвентаря, Цена Спорт-Инвентаря, Производитель Спорт-Инвентаря, Возрастная категория Спорт-Инвентаря\n" +
				"4 - Если возрастная категория будет меньше или равна 0, то товар будет доступен для всех возростных лиц\n" +
				"5 - Если цена товара будет меньше или равна 0, то товар будет считаться бесплатным\n" +
				"6 - Каждый пункт о товаре должен начинаться с новой строки\n" +
				"7 - Файл не должен иметь лишних пропусков (лишних пустых строк)\n" +
				"8 - Путь к файлу должен иметь слудующий формат: *:\\...\\*\\...\\*.* (Без кавычек)\n" +
				"При не соблюдении данных правил, программа может работать некорректно!\n");

		Console.WriteLine("Введите путь к файлу");

			while (true)
			{
				filePath = Console.ReadLine();
				fileProducts = new FileStream(filePath, FileMode.Open, FileAccess.Read);

				if (fileProducts.CanRead)
				{
					break;
				}
				Console.WriteLine("Введите корректный путь");
			}

			string[] textSplit = ReadFile(fileProducts);
			fileProducts.Close();

			int countProduct = 0;

			for (int i = 0; i < textSplit.Length; i++)
			{
				if (textSplit[i] == Product.productType[0] || textSplit[i] == Product.productType[1] || textSplit[i] == Product.productType[2])
				{
					countProduct++;
				}
			}

			if (countProduct == 0)
			{
				Console.WriteLine("В файле нету продуктов или файл заполнен неверно!");
				return products;
			}

			products = new Product[countProduct];
			string[] countStr = new string[5];

			int indexForWhile = 0;
			int typeIndex;

			for (int i = 0; i < products.Length; i++)
			{
				typeIndex = indexForWhile;

				for (int j = indexForWhile + 1; j < textSplit.Length; j++)
				{
					if (textSplit[j] == Product.productType[0] || textSplit[j] == Product.productType[1] || textSplit[j] == Product.productType[2])
					{
						indexForWhile = j;
						break;
					}
					for (int l = 0; l < countStr.Length; l++)
					{
						if (l == 4 && textSplit[indexForWhile] == Product.productType[2]) break;
						countStr[l] = textSplit[j];
						j++;
					}
					j--;
				}

				if (textSplit[typeIndex] == "Игрушки") products[i] = new Toy(countStr[0], Convert.ToSingle(countStr[1]), countStr[2], countStr[3], Convert.ToInt32(countStr[4]));
				else if (textSplit[typeIndex] == "Книги") products[i] = new Book(countStr[0], countStr[1], Convert.ToSingle(countStr[2]), countStr[3], Convert.ToInt32(countStr[4]));
				else if (textSplit[typeIndex] == "Спорт-Инвентарь") products[i] = new SportsEquipment(countStr[0], Convert.ToSingle(countStr[1]), countStr[2], Convert.ToInt32(countStr[3]));
				else Console.WriteLine("Не получилось добавить товар по индексу: {0}\nПроверье правописание типа продукта\n", typeIndex);
			}

			Console.WriteLine("\nДанные о продуктах записаны\n");
			return products;
		}

		static void Main()
		{
			try
			{
				Product[] products = SetProductsFromFile();
				int k;

				while (true)
				{
					Console.WriteLine("0 - Выход из программы\n" +
					"1 - Перезаписать данные о продуктах\n" +
					"2 - Вывести все товары\n" +
					"3 - Поиск товаров по типу");

					while (true)
					{
						k = Convert.ToInt32(Console.ReadLine());
						if (k >= 0 || k <= 3)
						{
							break;
						}
						Console.WriteLine("Введите корректное значение");
					}

					if (k == 0)
					{
						break;
					}
					else if (k == 1)
					{
						Console.WriteLine("Вы уверены, что хотите перезаписать данные о продуктах?\n1 - ДА\n0 - НЕТ");
						int i;

						while (true)
						{
							i = Convert.ToInt32(Console.ReadLine());
							if (i == 1 || i == 0)
							{
								break;
							}
							Console.WriteLine("Введите корректные данные");
						}
						if (i == 0)
						{
							break;
						}
						else
						{
							products = SetProductsFromFile();
						}
					}
					else if (k == 2)
					{
						if (products == null)
						{
							Console.WriteLine("Данные из файла не записаны!");
							break;
						}

						Console.WriteLine("\n\nВывод всех товаров");

						Product.GetAllProductsInfo(products);
					}
					else if (k == 3)
					{
						int strIndex;
						bool checkForNull = false;

						if (products == null)
						{
							Console.WriteLine("Нет данных о товарах!");
							break;
						}

						Console.WriteLine("Выберите тип\n1 - Игрушки\n2 - Книги\n3 - Спорт-Инвентарь\n");
						while (true)
						{
							strIndex = Convert.ToInt32(Console.ReadLine());
							if (strIndex == 1 || strIndex == 2 || strIndex == 3)
							{
								break;
							}
							Console.WriteLine("Введите корректные значения");
						}

						for (int i = 0; i < products.Length; i++)
						{
							if (products[i].MatchingTheType(Product.productType[strIndex - 1]))
							{
								checkForNull = true;
							}
						}

						if (!checkForNull)
						{
							Console.WriteLine("\nТоваров в данной категории не существует\n");
						}

						for (int i = 0; i < products.Length; i++)
						{
							if (products[i].MatchingTheType(Product.productType[strIndex - 1]))
							{
								products[i].GetProductInfo();
							}
						}
					}
					else
					{
						Console.WriteLine("\nВведите корректные значения!\n");
					}
				}
			}
			catch (FormatException)
			{
				Console.WriteLine("\nНеверный формат");
			}
			catch
			{
				Console.WriteLine("\nПроизошла ошибка, возможно вы ввели неправильный путь к файлу или же неправльно была сделана запись");
			}
		}
	}
}