pipeline {
	agent any
	
	environment {
		BuildConfiguration="Debug"
	}

	stages {
		stage('Build') {
			steps {
				sh "dotnet build"
			}
		}
		
		stage('Pack') {
			steps {
				sh 'dotnet pack PagingUtility/PagingUtility.csproj -c $BuildConfiguration --no-build --no-restore'
				sh 'dotnet pack PagingUtility.AsyncEnumerable/PagingUtility.AsyncEnumerable.csproj -c $BuildConfiguration --no-build --no-restore'
			}
		}
		
		stage('Push Nuget') {   
			steps {
				sh  'dotnet nuget push PagingUtility/bin/$BuildConfiguration/*.nupkg --source "raypro" --skip-duplicate'
				sh  'dotnet nuget push PagingUtility.AsyncEnumerable/bin/$BuildConfiguration/*.nupkg --source "raypro" --skip-duplicate'
			}
		}	
	}
}