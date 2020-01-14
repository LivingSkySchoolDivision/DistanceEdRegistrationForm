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
                    url: 'https://sourcecode.lskysd.ca/PublicCode/DistanceEdRegistrationForm.git'
            }
        }
        stage('Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/core/sdk:3.1"
                    args '-v ${PWD}:/app'
                }
            }
            steps {
                git branch: 'master',
                    url: 'https://sourcecode.lskysd.ca/PublicCode/DistanceEdRegistrationForm.git'

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