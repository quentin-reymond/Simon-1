<?php

declare(strict_types=1);

use App\Domain\User\UserRepository;
use App\Infrastructure\Persistence\User\MySqlUserRepository;
use DI\ContainerBuilder;

return function (ContainerBuilder $containerBuilder) {
    // On mappe l'interface UserRepository à son implémentation MySQL
    $containerBuilder->addDefinitions([
        UserRepository::class => \DI\autowire(MySqlUserRepository::class),
    ]);
};
