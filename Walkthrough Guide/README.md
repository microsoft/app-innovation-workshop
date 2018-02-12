![Banner](Assets/Banner.png)

# 1. Welcome  
Welcome to the Azure Mobile Workshop. Today you’re going to learn how to leverage your existing skills to build highly reliable backend systems to power a modern cross platform mobile app built using Xamarin.Forms. 

We’ve tried to make this app as production ready as possible and provide a good foundation for if you wish to pursue development in the future. Think of this as your development starter kit. Where we think theres area for improvement within the code base,you’ll find explanations about how to improve them. We've tried to pick non oppinionated frameworks to give you the flexibility to extend this app to fit your requirements. 

## 1.1 Prerequisites
This workshop is for intermediate to experienced C# developers who may lack either Azure or Xamarin experiance. This workshop is not for C# beginners and we expect you to know how design patterns such as Mvvm and MVC as well as having a basic understanding of the differences between developing Web APIs vs Mobile front-ends. 

If you are unfamiliar with the C# language, you can get started with a free course on the Internet. The basics of the language can be learned at www.learncs.org. Once you have the language basics under your belt, you can move on to building applications with Xamarin. You should be ready to follow this guided workshop, creating both the front-end and backend to run a field service app. 


## 1.2  Clone the project to your development machine
The entire solution is available to download from Github and licensed under MIT for use in your own projects. To clone the repository, head over to the [project on Github](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop) and select "Clone or download". We'll opt to clone to a local directory. Alternatively, if you're unfamilair with GIT version control systems then you may wish to download the solution as a ZIP file. 

![Cloning Solution from GitHub](Assets/CloneGITRepository.gif)


## 2. Mobile App
The mobile app currently run on both iOS and Android devices. UWP, macOS and Linux support should technically also work but they're outside the scope of todays learnings. 

![iPhone App Design](Assets/AppDesign.png)

### 2.1 Development SDK
The apps have been built with Xamarin.Forms targetting .NET Standard 2.0. You should find all your favourite .NET libraries will work with the both the backend (also targeting .NET Standard 2.0) and the mobile apps.  

### 2.2 Mvvm with FreshMvvm
We opted to use [FreshMvvm](https://github.com/rid00z/FreshMvvm) as our Mvvm library due to its small size and flexibility. Its specifically designed for Xamarin.Forms and offers lots of helpful extensions around navigation which we make full use of. 

## 3. Azure Architecture 
The backend architecture uses many Azure Services to handle things like authorisation, orchrestation, data storage, AI and anayltics. A high-level overview of the backend architecture cab be seen below. 

![Architecture Design](Assets/Architecture.png)

## Next Steps
[Architectures Overview](../03_Web_API/README.md)

##  Start Building your own version
* [Project Spec](Project-Spec.md)
* [Alternative Azure Architectures]()
* [Deploy your first Azure Resource]()


