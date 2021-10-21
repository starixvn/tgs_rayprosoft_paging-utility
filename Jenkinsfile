pipeline {
	agent any
	
	environment {
		BuildConfiguration="Debug"
	}

	stages {
		stage('Build') {
			steps {
				sh "dotnet build -c $BuildConfiguration"
			}
		}

		stage('Test') {
			steps {
				sh 'dotnet test -c $BuildConfiguration --logger trx --no-build'
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
				sh  'dotnet nuget push PagingUtility/bin/$BuildConfiguration/*.nupkg -s "raypro-baget" -k admin --skip-duplicate'
				sh  'dotnet nuget push PagingUtility.AsyncEnumerable/bin/$BuildConfiguration/*.nupkg -s "raypro-baget" -k admin --skip-duplicate'
			}
		}
	}

	post {
		always {
			step([$class: 'MSTestPublisher', testResultsFile:"**/*.trx", failOnError: false, keepLongStdio: true])
		}
	}
}