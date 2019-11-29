pipeline {
    agent any
    environment {
        REPO = "distanceed/distanceedregistration"
        PRIVATE_REPO = "${PRIVATE_DOCKER_REGISTRY}/${REPO}"
        TAG = "${BUILD_TIMESTAMP}"
    }
    stages {
        stage('Git clone') {
            steps {
                git branch: 'master',
                    credentialsId: 'JENKINS-AZUREDEVOPS',
                    url: 'git@ssh.dev.azure.com:v3/LivingSkySchoolDivision/DistanceEdReg/DistanceEdReg'
            }
        }
        stage('Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/core/sdk:3.0-buster'
                    args '-v ${PWD}:/app'
                }
            }
            steps {
                git branch: 'master',
                    credentialsId: 'JENKINS-AZUREDEVOPS',
                    url: 'git@ssh.dev.azure.com:v3/LivingSkySchoolDivision/DistanceEdReg/DistanceEdReg'

                dir("LSSDDistanceEdReg"){
                    sh 'dotnet build'
                    sh 'dotnet test'
                }
            }
        }
        stage('Docker build') {
            steps {
                dir("LSSDDistanceEdReg"){
                    sh "docker build --no-cache -t ${PRIVATE_REPO}:latest -t ${PRIVATE_REPO}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO}:${TAG}"
                sh "docker push ${PRIVATE_REPO}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}