pipeline {
	agent any
	
	environment {
		BuildConfiguration="Debug"
	}
	
	parameters {
		gitParameter branchFilter: 'origin/(.*)', defaultValue: 'master', name: 'BRANCH', type: 'PT_BRANCH'
	}

	stages {
		stage('Build') {
			steps {
				sh "dotnet build"
			}
		}
		
		stage('Pack') {
			steps {
				sh 'dotnet pack PagingToolkits/PagingToolkits.csproj -c $BuildConfiguration --no-build --no-restore'
				sh 'dotnet pack PagingToolkits.AsyncEnumerable/PagingToolkits.AsyncEnumerable.csproj -c $BuildConfiguration --no-build --no-restore'
			}
		}
		
		stage('Push Nuget') {   
			steps {
				sh  'dotnet nuget push PagingToolkits/bin/$BuildConfiguration/*.nupkg --source "raypro" --skip-duplicate'
				sh  'dotnet nuget push PagingToolkits.AsyncEnumerable/bin/$BuildConfiguration/*.nupkg --source "raypro" --skip-duplicate'
			}
		}	
	}
}