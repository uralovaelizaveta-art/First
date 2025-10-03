import os

def amout_strings(file):
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        f = file.readlines()
        return len(f)+1

def create():
    with open('notes.txt', 'a', encoding='utf-8') as file:
        file.writelines(f"{amout_strings(file)}. {input('Write your note:')}\n")
    return file
    
def delete():
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        deleted = int(input("What note do you want to delete? Write the number of it.\n"))-1
        list1 = file.readlines()
        
    with open('notes.txt', 'w', encoding ='utf-8') as file:
        a = 0
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        for i in range(len(list1)):
            if i < deleted:
                file.writelines(f"{i+1}. {list1[i][3:]}")
            elif i == deleted:
                continue
            else:
                file.writelines(f"{i}. {list1[i][3:]}")
        
def search(x):
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        print(file.readlines()[x-1])
    
        
def close():
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        print("Thank you, goodbye")
        file.close()
    
        
def show(): 
    with open('notes.txt', 'r+', encoding='utf-8') as file:
        for i in file.readlines():
            print(i)

        
def interface():
    print("Welcome to Mahager of Projects! Darling, let's start!")
    while True:
      print('''What do you want to do?
            1. create a new note
            2. delete a note
            3. search a note
            4. close the programm
            5. view all notes
        To select an action write the number of it.''')
      answer=input()
      match answer:
          case "1":
              create()
          case "2":
              delete()
          case "3":
              x = int(input("What note do you want to see& Write the number of it. \n"))
              search(x)
          case "4":
              close()
              break
          case "5":
              show()
          case _:
            print("You can choose only 1-5. Try again:")
            continue

interface()




